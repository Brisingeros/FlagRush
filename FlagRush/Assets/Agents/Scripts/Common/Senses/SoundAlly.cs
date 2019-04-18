using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAlly : Sense
{
    Nurse nurse;

    private void Start()
    {
        nurse = GetComponentInParent<Nurse>();
    }

    void OnTriggerEnter(Collider other)
    {

        Aspect aspect = other.GetComponent<Aspect>();

        if (aspect != null)
        {

            if (!aspect.alive && aspect.aspectAct == Aspect.aspect.Sound && aspect.teamAct == nurse.teamAct)
            {
                nurse.addSound(aspect);
                Debug.Log("Aliado herido escuchado");

            }

        }

    }

    private void OnTriggerExit(Collider other)
    {

        Aspect aspect = other.GetComponent<Aspect>();

        if (aspect != null)
        {

            if (!aspect.alive && aspect.aspectAct == Aspect.aspect.Sound && aspect.teamAct == nurse.teamAct)
            {
                nurse.removeSound(other.GetComponent<Aspect>());
            }

        }

    }

    void Update()
    {
    }

}
