using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grass : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Player pl = other.GetComponent<Player>();
        if (pl)
        {
            pl.setHidden(true);
            NavMeshAgent pAI = pl.getAgent();
            pAI.velocity = pAI.velocity * 0.7f;
            pAI.ResetPath();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player pl = other.GetComponent<Player>();
        if (pl)
        {
            pl.setHidden(false);
        }
    }
}