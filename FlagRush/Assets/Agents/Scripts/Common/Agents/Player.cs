using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class Player : Aspect {

    //TODO: Cuando pase X tiempo muerto, hacer despawn y spawnear una tumba en su lugar (Jeje solución de mierda a revivir sin querer)
    //TODO: Los enfermeros cuando escuchan un sonido o ven a un enemigo, se dirigen al escondite más cercano a un nivel inferior. Tras esto, reinician rutina

    private NavMeshAgent playerAI;
	public int defaultHealth;
	private int health;

	private WorldManager mG;

	List<Player> enemies;
    List<Aspect> enemiesSound;
    public Player focus;

	List<WayPoint> wP;

    private int actualLayerAnimator;

	// Use this for initialization
	void Start () {

        playerAI = GetComponent<NavMeshAgent>();
		aspectAct = aspect.NPC;
		health = defaultHealth;
		alive = true;
        enemies = new List<Player>();
        enemiesSound = new List<Aspect>();
        focus = null;
		wP = new List<WayPoint> ();

		mG = FindObjectOfType<WorldManager> ();
    }
	
	// Update is called once per frame
	void Update () {
		Debug.Log (wP.Count);
	}

    public void setLayerAnimator(int a)
    {
        actualLayerAnimator = a;
    }

    public int getActualLayerAnimator()
    {
        return actualLayerAnimator;
    }
    public NavMeshAgent getAgent()
    {
        return playerAI;

    }
	public Team.team getTeam(){
		return teamAct;
	}

	public void setTeam(Team.team tM){
		teamAct = tM;
	}

	public void getShot(){
		health--;
		alive = health > 0;
	}

	public void revive(){
		health = defaultHealth;
		alive = true;
	}

    public void addEnemy(Player e)
    {

        enemies.Add(e);

    }

    public virtual void addSound(Aspect a)
    {
        enemiesSound.Add(a);
    }

    public Player getEnemy(int pos)
    {
        return enemies[pos];
    }


    public bool removeEnemy(Player e)
    {
        return enemies.Remove(e);
    }

    public virtual bool removeSound(Aspect a)
    {
        return enemiesSound.Remove(a);
    }

    public void OrderByDistance(string type)
    {

        if(type.Equals("vision"))
            enemies = enemies.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToList();
        else if(type.Equals("sound"))
            enemiesSound = enemiesSound.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToList();

    }

    public int sizeEnemies()
    {
        return enemies.Count;
    }

    public int sizeEnemiesSound()
    {
        return enemiesSound.Count;
    }

    public float GetDistanceToEnemy(Player e)
    {
        return Vector3.Distance(e.transform.position, transform.position);
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("WayPoint")) {
			wP.Add (other.GetComponent<WayPoint> ());
		}
	}

	public WayPoint getObjective(){
	
		wP = wP.OrderBy (x => x.getValue (gameObject)).ToList();
		return wP [0];
	
	}
}
