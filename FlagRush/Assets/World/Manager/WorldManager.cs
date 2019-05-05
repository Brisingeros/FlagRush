﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	public int redMoral;
	public int blueMoral;

	private int redKillMoral;
	private int blueKillMoral;

	// Use this for initialization
	void Awake () {

        //gets values from Controller

        /*Controller c = FindObjectOfType<Controller>();
        redSoldiers = c.getRedSoldiers();
        blueSoldiers = c.getBlueSoldiers();
        redSnipers = c.getRedSnipers();
        blueSnipers = c.getBlueSnipers();
        redNurses = c.getRedNurses();
        blueNurses = c.getBlueNurses();*/

        //set initial values
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
			if (pl.getTypeNpc () == TypeNPC.type.Soldier)
				blueSoldiers--;
			break;

		case Team.team.Red:
			redMoral -= redKillMoral;
			blueMoral += blueKillMoral;
			redForces--;
			if (pl.getTypeNpc () == TypeNPC.type.Soldier)
				redSoldiers--;
			break;

		}

		if (redSoldiers <= 0) {
			GameOver ("BLUE");
		} else if (blueSoldiers <= 0) {
			GameOver ("RED");
		}
	}

	public void onRevive(Player pl){
		switch (pl.teamAct) {

		case Team.team.Blue:
			blueMoral += blueKillMoral;
			redMoral -= redKillMoral;
			blueForces++;
			if (pl.getTypeNpc () == TypeNPC.type.Soldier)
				blueSoldiers++;
			break;

		case Team.team.Red:
			redMoral += redKillMoral;
			blueMoral -= blueKillMoral;
			redForces--;
			if (pl.getTypeNpc () == TypeNPC.type.Soldier)
				redSoldiers++;
			break;

		}
	}

	public int getTeamMoral(Team.team tm){
		return (tm == Team.team.Blue) ? blueMoral : redMoral;
	}

    public void changeScene()
    {
        SceneManager.LoadScene("menu");
    }

	private void GameOver(string team){
		Debug.Log (team + " VICTORY");
        Invoke("changeScene", 5.0f);
	}
}
