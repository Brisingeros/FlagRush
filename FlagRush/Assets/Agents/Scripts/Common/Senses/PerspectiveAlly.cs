using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveAlly : Sense {

    Nurse nurse;

    private void Start()
    {
        nurse = GetComponentInParent<Nurse>();
    }

    void OnTriggerEnter(Collider other){
        Player al = other.GetComponent<Player>();

        if(al && al.aspectAct == Aspect.aspect.NPC && !al.alive)
        {
            nurse.addAlly(al);
            Debug.Log("Aliado avistado");
        }
	}

    private void OnTriggerExit(Collider other)
    {
        Player al = other.GetComponent<Player>();

        if ((al && al.aspectAct == Aspect.aspect.NPC && !al.alive)) // || other.CompareTag("tomb")
        {
            nurse.removeAlly(al);
        }
    }

	private void FixedUpdate()
    {
        nurse.removeSoldiers("ally");

        for(int i = 0; i < nurse.getAllySize(); i++)
        {
            Player p = nurse.getAlly(i);
            if (p.alive)
            {
                nurse.removeAlly(p);
                i--;
            }
        }
    }
}
