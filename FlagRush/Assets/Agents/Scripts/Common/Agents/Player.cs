using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public abstract class Player : Aspect {

    protected NavMeshAgent playerAI;
	protected Animator anim;

	protected int defaultHealth;

	protected TypeNPC.type typeNpc;

	protected WorldManager mG;
	protected SoundGenerator sG;

	protected List<Player> enemies;
    protected List<Aspect> enemiesSound;

    public Player focus;

	protected List<WayPoint> wP;

    protected int actualLayerAnimator;
    protected bool hidden = false;
	public GameObject basicSound;

	private GameObject tomb;
	protected float speedMax;

	void Awake() {
		mG = FindObjectOfType<WorldManager> ();
		sG = FindObjectOfType<SoundGenerator> ();
		tomb = Resources.Load<GameObject>("Prefabs/Tomb");

		playerAI = GetComponent<NavMeshAgent>();
		speedMax = playerAI.speed;
		anim = GetComponent<Animator> ();
		aspectAct = aspect.NPC;
		alive = true;
		enemies = new List<Player>();
		enemiesSound = new List<Aspect>();
		focus = null;
		wP = new List<WayPoint> ();

		initPlayer();
	}

    protected abstract void initPlayer();

    public abstract void removeSoldiers(string type);
    public abstract void removeDestroyedSounds(string type);

    public Animator getAnimator()
    {
        return anim;
    }
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

    public Vector3 findHidingPlace()
    {
		//playerAI.ResetPath();
        List<GameObject> bushes = GameObject.FindGameObjectsWithTag("Bush").ToList();
        bushes = bushes.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToList(); //ordenamos por distancia a enfermera
		Debug.Log(bushes[0]);

		return bushes [0].transform.position;
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

    public Rigidbody GetRigidbody()
    {
        return GetComponent<Rigidbody>();
    }
	public void getShot(){
		int livesPrev = anim.GetInteger ("Lives");
		alive = livesPrev-1 > 0;
		int lives = (alive) ? livesPrev-1 : 0;
		anim.SetInteger("Lives", lives);

		if (!alive && lives < livesPrev)
			mG.onKill (this);
    }

	public void die(){
		Vector3 posPlayer = transform.position;
		posPlayer.y = 0;

		Instantiate(tomb);
		tomb.transform.position = posPlayer;

		Destroy(gameObject);
	}

    public void revive(){
		anim.SetInteger("Lives", defaultHealth);
		alive = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        GetComponent<Rigidbody>().freezeRotation = false;

        if (GetComponentInChildren<Aspect>())
        {
            GameObject sound = transform.GetChild(transform.childCount-1).gameObject;
            Destroy(sound);
        }

		mG.onRevive (this);
	}

    private void LateUpdate()
    {
        if (alive && playerAI.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(playerAI.velocity.normalized);
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
			if (w.team == teamAct && w.type == typeNpc) {
				wP.Add (w);
				if (!anim.enabled)
					anim.enabled = true;
			}
		}
    }

    public WayPoint getObjective(){
		wP = wP.OrderBy (x => x.getValue (gameObject)).ToList();
		return wP [0];
	}

	public Aspect getEnemySound() {
		return (enemiesSound.Count > 0) ? enemiesSound[0] : null;
	}

	public virtual void generateSound(){
        hidden = false;

		GameObject snd = sG.Initialize (transform.position, teamAct, alive);
		if(!alive)
			snd.transform.SetParent(transform);

    }

	public float getSpeedMax(){
		return speedMax;
	}
}
