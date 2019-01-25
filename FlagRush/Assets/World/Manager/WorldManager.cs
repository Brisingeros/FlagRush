using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public int redSoldiers;
	public int redSnipers;
	public int redNurses;

	public int blueSoldiers;
	public int blueSnipers;
	public int blueNurses;

	private int redMoral;
	private int blueMoral;

	private int redKillMoral;
	private int blueKillMoral;

	// Use this for initialization
	void Start () {
		redMoral = blueMoral = 100;
		redKillMoral = redMoral / (redSoldiers + redSnipers + redNurses);
		blueKillMoral = blueMoral / (blueSoldiers + blueSnipers + blueNurses);
	}
	
	public void onKill(Team.team teamDeath){
		switch (teamDeath) {

		case Team.team.Blue:
			blueMoral -= blueKillMoral;
			redMoral += redKillMoral;
			break;

		case Team.team.Red:
			redMoral -= redKillMoral;
			blueMoral += blueKillMoral;
			break;

		}
	}

	public void onRevive(Team.team teamRevival){
		switch (teamRevival) {

		case Team.team.Blue:
			blueMoral += blueKillMoral;
			redMoral -= redKillMoral;
			break;

		case Team.team.Red:
			redMoral += redKillMoral;
			blueMoral -= blueKillMoral;
			break;

		}
	}
}
