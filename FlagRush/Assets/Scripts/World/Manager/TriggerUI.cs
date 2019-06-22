using System.Collections.Generic;
using UnityEngine;

public class TriggerUI : MonoBehaviour {

    public Team.team teamAct;
    public Arrow teamArrow;

    private List<Player> enemies = new List<Player>();
    private Arrow arrowInstance;
    private float elapsedTime = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if ( p != null && p.GetComponent<Soldier>() != null && p.getTeam() != teamAct)
        {
            if (enemies.Count == 0) {
                arrowInstance = Instantiate(teamArrow);
                arrowInstance.transform.position = new Vector3(transform.position.x, transform.position.y + 25, transform.position.z); ;
            }
            enemies.Add(p);
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime >= 5.0f)
        {
            elapsedTime = 0.0f;
            enemies = enemies.FindAll(x => x != null && x.alive);
            if (enemies.Count == 0 && arrowInstance)
                Destroy(arrowInstance.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null && p.GetComponent<Soldier>() != null && p.getTeam() != teamAct)
        {
            enemies.Remove(p);
            if (enemies.Count == 0 && arrowInstance)
                Destroy(arrowInstance.gameObject);
        }
    }
}
