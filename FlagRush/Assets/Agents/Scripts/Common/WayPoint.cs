﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {

	public static int weight;

	public WayPoint previous;
	public WayPoint next;
    public Type type;
    public Team team;

    public enum Type {

        nurse,
        soldier,
        sniper

    };

    public enum Team
    {
        red,
        blue

    }

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
        if (player)
        {
            Animator anim = player.GetComponent<Animator>();
            int layerActual = player.getActualLayerAnimator();

            if (anim.GetCurrentAnimatorStateInfo(layerActual).IsName("Avanzar"))
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
