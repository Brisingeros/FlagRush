using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public int redSoldiers;
	public int redSnipers;
	public int redNurses;

	public Spawner redSpawner;
	public Spawner blueSpawner;

	public Barricade redBarricade;
	public Barricade blueBarricade;

	public GameObject redSoldierPrefab;
	public GameObject blueSoldierPrefab;
	public GameObject redSniperPrefab;
	public GameObject blueSniperPrefab;
	public GameObject redNursePrefab;
	public GameObject blueNursePrefab;

	private int redForces;

	public int blueSoldiers;
	public int blueSnipers;
	public int blueNurses;
	private int blueForces;

	private int redMoral;
	private int blueMoral;

	private int redKillMoral;
	private int blueKillMoral;

	// Use this for initialization
	void Start () {
		redForces = redSoldiers + redSnipers + redNurses;
		blueForces = blueSoldiers + blueSnipers + blueNurses;
		redMoral = blueMoral = 100;
		redKillMoral = redMoral / (redForces);
		blueKillMoral = blueMoral / (blueForces);

		Task[] promises = new Task[2];

		promises[0] = Task.Run (() => { redBarricade.spawnSnipers(redSniperPrefab, redSnipers); });
		promises[1] = Task.Run (() => { blueBarricade.spawnSnipers(blueSniperPrefab, blueSnipers); });
		Task.WaitAll (promises);

		promises = new Task[2];

		promises[0] = Task.Run (() => { redSpawner.SpawnPrefab(redSoldierPrefab, redSoldiers); });
		promises[1] = Task.Run (() => { blueSpawner.SpawnPrefab(blueSoldierPrefab, blueSoldiers); });
		Task.WaitAll (promises);

		promises = new Task[2];

		promises[0] = Task.Run (() => { redSpawner.SpawnPrefab(redNursePrefab, redNurses); });
		promises[1] = Task.Run (() => { blueSpawner.SpawnPrefab(blueNursePrefab, blueNurses); });
		Task.WaitAll (promises);

	}
	
	public void onKill(Player pl){
		switch (pl.teamAct) {

		case Team.team.Blue:
			blueMoral -= blueKillMoral;
			redMoral += redKillMoral;
			blueForces--;
			break;

		case Team.team.Red:
			redMoral -= redKillMoral;
			blueMoral += blueKillMoral;
			redForces--;
			break;

		}

		if (redForces <= 0) {
			GameOver ("BLUE");
		} else if (blueForces <= 0) {
			GameOver ("RED");
		}
	}

	public void onRevive(Player pl){
		switch (pl.teamAct) {

		case Team.team.Blue:
			blueMoral += blueKillMoral;
			redMoral -= redKillMoral;
			blueForces++;
			break;

		case Team.team.Red:
			redMoral += redKillMoral;
			blueMoral -= blueKillMoral;
			redForces--;
			break;

		}
	}

	private void GameOver(string team){
		Debug.Log (team + " VICTORY");

		#if UNITY_EDITOR
		{
			UnityEditor.EditorApplication.isPlaying = false;
		}
		#else
		{
		Application.Quit ();
		}
		#endif
	}
}
