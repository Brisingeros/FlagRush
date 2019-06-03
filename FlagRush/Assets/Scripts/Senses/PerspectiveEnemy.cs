using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveEnemy : Sense {

	public GameObject shootPos;
    private Player parent;
	private TypeNPC.type[] targetTypes;
	private float distanceMelee = 8;

    private void Awake()
    {
        parent = transform.GetComponentInParent<Player>();
		targetTypes = parent.getTypeTargets ();
    }

    void OnTriggerEnter(Collider other){
        Player p = other.GetComponent<Player>();
		if (p && p.aspectAct == Aspect.aspect.NPC && p.teamAct != pla.teamAct && assertType(p.getTypeNpc()))
            parent.addEnemy(p);
	}

	private bool assertType(TypeNPC.type typeEnemy){
		int index = 0;
		bool isIn = false;

		do {
			isIn = targetTypes[index] == typeEnemy;
			index++;
		} while(index < targetTypes.Length && !isIn);

		return isIn;
	}

	private void FixedUpdate()
	{
		parent.removeSoldiers ("enemy");

        //El array de enemigos estará ordenado por distancia (recta)
		parent.setFocus(null);
        parent.OrderByDistance("vision");
        int numEnemies = parent.sizeEnemies();
        //Probar raycast en orden, y quedarse como focus con el primero que puedas golpear
        //si la distancia en líne recta es menor a *insert number* es un golpe a melee, no hace falta raycast
        if ( numEnemies > 0)
        {
            int i = 0;
			while(i < numEnemies && parent.getFocus() == null)
            {
                Player player = parent.getEnemy(i);

				if (player && (parent.getHidden() || !player.getHidden()) && player.alive)
                {
                    if(parent.GetDistanceToEnemy(player) < distanceMelee)
                    {
						parent.setFocus(player);
                    }
                    else
                    {
                        RaycastHit hit;
						Vector3 rayDirection = Vector3.zero;
						rayDirection = player.transform.position - shootPos.transform.position;

						if (rayDirection != Vector3.zero && Physics.Raycast(shootPos.transform.position, rayDirection, out hit, 300.0f, -5, QueryTriggerInteraction.Ignore))
                        {
							Player plaHit = hit.collider.GetComponentInParent<Player> ();

                            if (plaHit != null && plaHit == player)
								parent.setFocus(player);
                        }
                    }
                        
                }
                i++;
            }
        }
    }

	void OnTriggerExit(Collider other){
        Player p = other.GetComponent<Player>();

        if (p && p.aspectAct == Aspect.aspect.NPC && p.teamAct != pla.teamAct)
            parent.removeEnemy(other.GetComponent<Player>());
	}

}
