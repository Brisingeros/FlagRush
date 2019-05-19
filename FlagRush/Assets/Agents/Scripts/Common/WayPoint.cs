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
		return Mathf.Abs(dis) - (lvl * weight);
	}

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
		if (player && (team == player.getTeam() && type == player.getTypeNpc()))
        {
            Animator anim = player.GetComponent<Animator>();
            int layerActual = player.getActualLayerAnimator();

			if (anim.GetCurrentAnimatorStateInfo (layerActual).IsName ("Avanzar") && getNext () != null)
				player.getAgent().SetDestination(getNext().transform.position);
        }

		if (other.tag.Equals ("WaypointVision")) {
			player = other.GetComponentInParent<Player> ();
			if (team == player.getTeam() && type == player.getTypeNpc())
				player.addWayPoint (this);
		}
    }

	private void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals ("WaypointVision")) {
			Player player = other.GetComponentInParent<Player> ();
			if (team == player.getTeam() && type == player.getTypeNpc())
				player.removeWayPoint (this);
		}
	}

	public WayPoint getNext(){
		return (next != null) ? next : this;
	}

}
