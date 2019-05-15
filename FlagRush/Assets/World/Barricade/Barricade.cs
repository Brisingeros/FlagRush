using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour {

	private readonly int[] indexDefend = {0,4,8,12};
	private readonly int[] indexAttack = {1,2,3,5,6,7,9,10,11};
	public PositionBarricade[] positions = new PositionBarricade[13];
	private bool[] occupied = {false,false,false,false,false,false,false,false,false,false,false,false,false};

	private SoundGenerator sG;

	// Use this for initialization
	void Start () {
		sG = FindObjectOfType<SoundGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 getPositionMarker(int pos){
		return positions [pos].getMarker ();
	}

	public PositionBarricade defend(Sniper sniper){
		int pos = sniper.positionBarricade;
		int betweenBottom = pos / 4;
		int auxStart;

        if (assertPos(indexDefend, pos))
        {
            Debug.Log("ERROR DEFEND ON DEFENSE");
            return null;
        }

        if (pos % 2 == 0)
        { //Central
            auxStart = betweenBottom + ((int)Random.Range(0, 2));
        }
        else
        {
            if ((pos / 2) % 2 == 0)
            { //Izq
                auxStart = betweenBottom;
            }
            else
            { //Der
                auxStart = betweenBottom + 1;
            }
        }
			
        if ((auxStart * 4 < occupied.Length) && !occupied[auxStart * 4])
        {////////////////////////////
            occupied[auxStart * 4] = true;
            occupied[pos] = false;
            sniper.positionBarricade = auxStart * 4;
            return positions[auxStart * 4];
        }

        ////////////////////////
        List<int> orderLook = new List<int>();
        int index = 0;
        int auxStart2;

        if (auxStart > betweenBottom)
        { //Empezar por la derecha
            auxStart2 = betweenBottom;

            while (orderLook.Count < indexDefend.Length)
            {
                if ((auxStart + index) < indexDefend.Length)
                    orderLook.Add(indexDefend[auxStart + index]);

                if ((auxStart2 - index) >= 0)
                    orderLook.Add(indexDefend[auxStart2 - index]);

                index++;
            }

        }
        else
        { //Empezar por la izquierda
            auxStart2 = auxStart + 1;

            while (orderLook.Count < indexDefend.Length)
            {
                if ((auxStart - index) >= 0)
                    orderLook.Add(indexDefend[auxStart - index]);

                if ((auxStart2 + index) < indexDefend.Length)
                    orderLook.Add(indexDefend[auxStart2 + index]);

                index++;
            }
        }

        ////////////////////////
        while (orderLook.Count > 0)
        {
            int posLooking = orderLook[0];
            orderLook.RemoveAt(0);

            if (!occupied[posLooking])
            {
                occupied[posLooking] = true;
                occupied[pos] = false;
                sniper.positionBarricade = posLooking;
                return positions[posLooking];
            }
        }

        return null;
        
	}

	public PositionBarricade attack(Sniper sniper){
		int pos = sniper.positionBarricade;

        if (assertPos(indexAttack, pos))
        {
            Debug.Log("ERROR ATTACK ON ATTACK");
            return null;
        }

        int[] auxArr = SuffleArray (indexAttack);

		for (int i = 0; i < auxArr.Length; i++) {
			if (!occupied [auxArr [i]]) {
				occupied [pos] = false;
				occupied [auxArr [i]] = true;
                sniper.setHidden(false);
                sniper.positionBarricade = auxArr [i];
				return positions [auxArr [i]];
			}
		}

		return null;
	}

	private int[] SuffleArray(int[] array){
		int[] auxArray = new int[array.Length];

		for (int i = 0; i < array.Length; i++) {
			auxArray [i] = array [i];
		}

		for (int j = auxArray.Length; j > 0; j--) {
			int k = (int)Random.Range (0, j);
			int l = auxArray [k];
			auxArray [k] = array [j - 1];
			auxArray [j - 1] = l;
		}

		return auxArray;
	}

    private bool assertPos(int[] arr, int pos) {
        bool inside = false;
        int index = 0;

        while (!inside && index < arr.Length) {
            inside = arr[index] == pos;

            index++;
        }

        return inside;
    }

	public GameObject generateSound(Sniper snp, int posBarricade){
		Vector3 posSound = getPositionMarker (posBarricade);

		GameObject snd = sG.Initialize (posSound, snp.getTeam(), snp.alive);
		snd.transform.position = new Vector3(snd.transform.position.x, 1, snd.transform.position.z);

		return snd;
	}

	/////////////////////////////////////////////////////////////
	public void spawnSnipers(GameObject prefab, int numSnipers){

		int[] orderSpawn = SuffleArray (indexAttack);
		int index = 0;

		while (index < numSnipers && index < orderSpawn.Length) {
			int position = orderSpawn [index];
			occupied [position] = true;

			GameObject instanced = Instantiate (prefab);
            instanced.transform.position = transform.TransformPoint(positions[position].transform.localPosition);
            instanced.GetComponent<Player>().getAgent().Warp(instanced.transform.position);

            Sniper instancedSnp = instanced.GetComponent<Sniper> ();
			instancedSnp.positionBarricade = position;
			instancedSnp.barricade = this;

			Vector3 focusLeveled = getPositionMarker(position);
			focusLeveled.y = instanced.transform.position.y;
			instanced.transform.LookAt (focusLeveled);

			index++;
		}
	}

}
