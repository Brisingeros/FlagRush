using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {

	public static int weight;

	public WayPoint previous;
	public WayPoint next;

	public int lvl;

	public float getValue(GameObject g){
		float dis =  Vector3.Distance(g.transform.position, this.transform.position);

		return Mathf.Abs(dis) - (lvl * weight);
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
        if (player)
        {
            Animator anim = player.GetComponent<Animator>();
            int layerActual = player.getActualLayerAnimator();

            if (anim.GetCurrentAnimatorStateInfo(layerActual).IsName(""))
            {
                
                player.getAgent().SetDestination(getNext().transform.position);

            }

        }
    }
    public WayPoint getNext(){
		WayPoint aux = this;

		if (next != null)
			aux = next;

		return aux;
	}

}
