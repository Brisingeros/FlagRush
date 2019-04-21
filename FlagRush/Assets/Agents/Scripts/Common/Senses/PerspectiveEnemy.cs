using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveEnemy : Sense {

	public GameObject shootPos;
    private Player parent;
    private float distanceMelee = 3; //CAMBIAR

    private void Awake()
    {
        parent = transform.GetComponentInParent<Player>();

    }

    void OnTriggerEnter(Collider other){

        Player p = other.GetComponent<Player>();
        if (p && p.aspectAct == Aspect.aspect.NPC && p.teamAct != pla.teamAct)
            parent.addEnemy(p);
		//insert to enemies list

	}

    private void Update()
    {
        parent.removeSoldiers("enemy");

        //El array de enemigos estará ordenado por distancia (recta)
        parent.focus = null;
        parent.OrderByDistance("vision");
        int numEnemies = parent.sizeEnemies();
        //Probar raycast en orden, y quedarse como focus con el primero que puedas golpear
        //si la distancia en líne recta es menor a *insert number* es un golpe a melee, no hace falta raycast
        if ( numEnemies > 0)
        {
            int i = 0;
            while(i < numEnemies && parent.focus == null)
            {
                Player player = parent.getEnemy(i);

                if (player && !player.getHidden() && player.alive)
                {

                    if(parent.GetDistanceToEnemy(player) < distanceMelee)
                    {
                        //Debug.Log("Enemigo a la vista. Golpe melee");
                        parent.focus = player;

                    }
                    else
                    {
                        RaycastHit hit;
                        Vector3 rayDirection = player.transform.position - shootPos.transform.position;

						if (Physics.Raycast(shootPos.transform.position, rayDirection, out hit, 300.0f, -5, QueryTriggerInteraction.Ignore))
                        { //Mirar si es necesario maxdistance
							Player plaHit = hit.collider.GetComponentInParent<Player> ();

                            if (plaHit != null && plaHit == player)
                            {
                               // Debug.Log("Enemigo a la vista");
                                parent.focus = player;
                            }
                        }
                    }
                        
                }

                i++;

            }

        }

    }

	void OnTriggerExit(Collider other){
        //Remove from enemies list
        Player p = other.GetComponent<Player>();

        if (p && p.aspectAct == Aspect.aspect.NPC && p.teamAct != pla.teamAct)
            parent.removeEnemy(other.GetComponent<Player>());
	}

}
