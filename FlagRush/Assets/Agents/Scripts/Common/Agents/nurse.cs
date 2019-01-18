using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Nurse : Player {

    List<Aspect> allySounds;
    List<Player> allies;
    // Use this for initialization
    void Start () {
        allySounds = new List<Aspect>();
        allies = new List<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addAlly(Player a)
    {
        allies.Add(a);
    }

    public bool removeAlly(Player a)
    {
        return allies.Remove(a);

    }

    public override void addSound(Aspect a){
        allySounds.Add(a);
    }

    public override bool removeSound(Aspect a)
    {
        return allySounds.Remove(a);
    }

    public void OrderAlliesByDistance(string type)
    {
        if(type.Equals("vision"))
            allies = allies.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToList();
        else if(type.Equals("sound"))
            allySounds = allySounds.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToList();
    }

    public Player getAlly(int pos)
    {
        return allies[pos];
    }
    public int getAllySize()
    {
        return allies.Count;
    }
}
