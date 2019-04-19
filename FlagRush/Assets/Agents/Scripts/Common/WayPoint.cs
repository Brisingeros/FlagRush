using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {

	public static int weight;

	public WayPoint previous;
	public WayPoint next;
	public TypeNPC.type type;
    public Team.team team;

	public int lvl;

	public float getValue(GameObject g){
		float dis =  Vector3.Distance(g.transform.position, transform.position);

		return Mathf.Abs(dis) - (lvl * weight); //cuanto mas nivel, mas cerca de la bandera

	}

	public WayPoint getPrevious(){
		WayPoint aux = this;

		if (previous != null)
			aux = previous;

		return aux;
	}

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
		if (player && (team == player.getTeam() && type == player.getTypeNpc()))
        {
            Animator anim = player.GetComponent<Animator>();
            int layerActual = player.getActualLayerAnimator();

            if (anim.GetCurrentAnimatorStateInfo(layerActual).IsName("Avanzar"))
            {
				if (getNext () != null)
				{
					//player.transform.LookAt(getNext().transform);
					player.getAgent().SetDestination(getNext().transform.position);
				}
            }else if (CompareTag("Bush")) //es un escondite, ha llegado
            {
                player.setHidden(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player && (team == player.getTeam() && type == player.getTypeNpc()))
        {
            if (CompareTag("Bush")) //es un escondite, ha llegado
            {
                player.setHidden(false);
            }

        }
    }
    public WayPoint getNext(){
		return next;
	}

}
