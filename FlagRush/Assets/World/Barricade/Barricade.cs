using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour {

	private readonly int[] indexDefend = {0,4,8,12};
	private readonly int[] indexAttack = {1,2,3,5,6,7,9,10,11};
	public PositionBarricade[] positions = new PositionBarricade[12];
	public bool[] occupied = {false,false,false,false,false,false,false,false,false,false,false,false};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public PositionBarricade defend(int pos){
		int betweenBottom = pos / 4;
		int auxStart;

		if (pos % 2 == 0) { //Central
			auxStart = betweenBottom + ((int) Random.Range(0,2));
		} else {
			if ((pos / 2) % 2 == 0) { //Izq
				auxStart = betweenBottom;
			} else { //Der
				auxStart = betweenBottom+1;
			}
		}

		if (!occupied [auxStart * 4]) {
			occupied [auxStart * 4] = true;
			return positions[auxStart * 4];
		}

		////////////////////////
		List<int> orderLook = new List<int>();
		int index = 0;
		int auxStart2;

		if (auxStart > betweenBottom) { //Empezar por la derecha
			auxStart2 = betweenBottom;

			while (orderLook.Count < 4) {
				if ((auxStart + index) < indexDefend.Length)
					orderLook.Add (indexDefend[auxStart+index]);

				if ((auxStart2 - index) > 0)
					orderLook.Add (indexDefend[auxStart2-index]);

				index++;
			}

		} else { //Empezar por la izquierda
			auxStart2 = auxStart + 1;

			while (orderLook.Count < 4) {
				if ((auxStart - index) > 0)
					orderLook.Add (indexDefend[auxStart-index]);

				if ((auxStart2 + index) < indexDefend.Length)
					orderLook.Add (indexDefend[auxStart2+index]);

				index++;
			}
		}

		////////////////////////
		while (orderLook.Count > 0){
			int posLooking = orderLook[0];
			orderLook.RemoveAt(0);

			if (!occupied [posLooking]) {
				occupied [posLooking] = true;
				occupied [pos] = false;
				return positions [posLooking];
			}
		}

		return null;
	}

	public PositionBarricade attack(int pos){
		int[] auxArr = SuffleArray (indexAttack);

		for (int i = 0; i < auxArr.Length; i++) {
			if (!occupied [auxArr [i]]) {
				occupied [pos] = false;
				occupied [auxArr [i]] = true;
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
			int k = (int)Random.Range (0, j + 1);
			int l = auxArray [k];
			auxArray [k] = array [j - 1];
			auxArray [j - 1] = l;
		}

		return auxArray;
	}

}
