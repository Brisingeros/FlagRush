using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Nurse : Player {

    List<Aspect> allySounds;
    List<Player> allies;
    public Player focusAlly;

    protected override void initPlayer()
    {
        defaultHealth = 50;
        anim.SetInteger("Lives", defaultHealth);
		typeNpc = TypeNPC.type.Nurse;

        allySounds = new List<Aspect>();
        allies = new List<Player>();

    }

    // Update is called once per frame
    void Update () {

		bool huir = anim.GetBool("Peligro");
        Debug.Log("sonidos aliados: " + allySounds.Count);
        anim.SetBool("Alerta", allySounds.Count > 0);
        anim.SetBool("Aliado", allies.Count > 0);

        if (!huir) {

            huir = focus != null;

            if (!huir && enemiesSound.Count > 0)
            {
                huir = Vector3.Distance(enemiesSound[0].transform.position, transform.position) < 40;
            }
            anim.SetBool ("Peligro", huir); //en el statemachine de huir hay que ponerlo a false cuando llegue al waypoint
		}

		//fijar peligro según la distancia a sonidos enemigos o si hay un enemigo visible
		//El problema de este, es que aquí nunca debería ponerse a false si estaba a true,
		//dado que lo que queremos es que el enfermero huya hasta el waypoint anterior, y ahí realizar de nuevo comprobación
	}

    public void findHidingPlace()
    {

        List<GameObject> bushes = GameObject.FindGameObjectsWithTag("Bush").ToList();
        bushes = bushes.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToList(); //ordenamos por distancia a enfermera
        getAgent().SetDestination(bushes[0].transform.position);

    }

    public void SetFocus()
    {
        focusAlly = allies.Count > 0?allies[0]: null;
    }

    public void addAlly(Player a)
    {
        allies.Add(a);
        OrderAlliesByDistance("vision");

    }

    protected override void removeSoldiers(Team.team type)
    {
        if (type != teamAct)
        {
            enemies = enemies.FindAll(x => x != null);

        }
        else
        {
            allies = allies.FindAll(x => x != null);
        }
    }

    public void removeDestroyedSounds(Team.team type)
    {

        if (type != teamAct)
        {
            enemiesSound = enemiesSound.FindAll(x => x != null);

        }else
        {
            allySounds = allySounds.FindAll(x => x != null);
        }

    }

    public bool removeAlly(Player a)
    {
        bool success = allies.Remove(a);
        OrderAlliesByDistance("vision");

        return success;
    }

    public override void addSound(Aspect a){
        allySounds.Add(a);
    }

    public override bool removeSound(Aspect a)
    {
        Debug.Log("elimina sonido");
        return allySounds.Remove(a);
    }

    public void HelpAlly()
    {
        OrderAlliesByDistance("sound");
        Aspect goal = allySounds.Count > 0? getAllySound(0): null;
        if (goal != null)
            getAgent().SetDestination(goal.transform.position);
        else
            removeSound(goal);
    }

    public void OrderAlliesByDistance(string type)
    {
        if (type.Equals("vision"))
        {
            allies = allies.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToList();
            SetFocus();
        }
        else if (type.Equals("sound"))
            allySounds = allySounds.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToList();
    }

    public Aspect getAllySound(int pos)
    {
        return allySounds[pos];
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
