using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public abstract class Player : Aspect {

    //TODO: Cuando pase X tiempo muerto, hacer despawn y spawnear una tumba en su lugar (Jeje solución de mierda a revivir sin querer)
    //TODO: Los enfermeros cuando escuchan un sonido o ven a un enemigo, se dirigen al escondite más cercano a un nivel inferior. Tras esto, reinician rutina

    protected NavMeshAgent playerAI;
	protected Animator anim;

	protected int defaultHealth;

	protected TypeNPC.type typeNpc;

	protected WorldManager mG;

	protected List<Player> enemies;
    protected List<Aspect> enemiesSound;

    public Player focus;

	protected List<WayPoint> wP;

    protected int actualLayerAnimator;
    protected bool hidden = false;
	public GameObject basicSound;

	void Start () {

        playerAI = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator> ();
		aspectAct = aspect.NPC;
		alive = true;
        enemies = new List<Player>();
        enemiesSound = new List<Aspect>();
        focus = null;
		wP = new List<WayPoint> ();

		mG = FindObjectOfType<WorldManager> ();

		initPlayer();
    }

    protected abstract void initPlayer();

    public abstract void removeSoldiers(string type);
    public abstract void removeDestroyedSounds(string type);
    /*void Update () {
		
	}*/

    public void setHidden(bool state)
    {
        hidden = state;
    }

    public bool getHidden()
    {
        return hidden;
    }

	public TypeNPC.type getTypeNpc(){
		return typeNpc;
	}

    public void findHidingPlace()
    {
        getAgent().ResetPath();
        List<GameObject> bushes = GameObject.FindGameObjectsWithTag("Bush").ToList();
        bushes = bushes.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToList(); //ordenamos por distancia a enfermera
        getAgent().SetDestination(bushes[0].transform.position);

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
		int lives = anim.GetInteger ("Lives");
		lives--;
		alive = lives > 0;
		anim.SetInteger("Lives", lives);
    }

    public void revive(){
		anim.SetInteger("Lives", defaultHealth);
		alive = true;
        if (GetComponentInChildren<Aspect>())
        {
            GameObject sound = transform.GetChild(transform.childCount-1).gameObject;
            Destroy(sound);
        }
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
            WayPoint w = other.GetComponent<WayPoint>();
			if (w.team == teamAct && w.type == typeNpc)
			    wP.Add (other.GetComponent<WayPoint> ());
		}
    }

    public WayPoint getObjective(){
	
		wP = wP.OrderBy (x => x.getValue (gameObject)).ToList();
		return wP [0];
	
	}

	public Aspect getEnemySound() {
		return (enemiesSound.Count > 0) ? enemiesSound[0] : null;
	}

	public void generateSound(){
        hidden = false;
		GameObject snd = Instantiate(basicSound);
		snd.transform.position = transform.position;

		Sound sndComp = snd.GetComponent<Sound> ();
		sndComp.teamAct = teamAct;
		sndComp.alive = alive;

        if(!alive)
            snd.transform.SetParent(transform);

    }
}
