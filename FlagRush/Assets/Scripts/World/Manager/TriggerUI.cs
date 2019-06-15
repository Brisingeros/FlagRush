using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUI : MonoBehaviour {

    public Team.team teamAct;

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if ( p != null && p.GetComponent<Soldier>() != null && p.getTeam() != teamAct)
        {
            Debug.Log("Hi");
            FindObjectOfType<WorldManager>().arrivingToGoal(p.getTeam());

        }
    }
}
