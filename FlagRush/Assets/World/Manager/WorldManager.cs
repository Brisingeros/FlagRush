using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WorldManager : MonoBehaviour {

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

	public int redSoldiers;
	public int redSnipers;
	public int redNurses;
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
	void Awake () {
		redForces = redSoldiers + redSnipers + redNurses;
		blueForces = blueSoldiers + blueSnipers + blueNurses;
		redMoral = blueMoral = 100;
		redKillMoral = redMoral / (redForces);
		blueKillMoral = blueMoral / (blueForces);

		redBarricade.spawnSnipers(redSniperPrefab, redSnipers);
		blueBarricade.spawnSnipers(blueSniperPrefab, blueSnipers);

		StartCoroutine (soldiers(redSpawner, redSoldierPrefab, redSoldiers));
		StartCoroutine (soldiers(blueSpawner, blueSoldierPrefab, blueSoldiers));
		StartCoroutine (soldiers(redSpawner, redNursePrefab, redNurses));
		StartCoroutine (soldiers(blueSpawner, blueNursePrefab, blueNurses));
	}

	IEnumerator soldiers(Spawner target, GameObject prefab, int num){
		for (int i = 0; i < num; i++) {
			target.SpawnPrefab (prefab, 1);
			yield return null;
		}
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
