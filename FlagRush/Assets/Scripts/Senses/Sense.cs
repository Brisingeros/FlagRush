using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sense : MonoBehaviour {

	protected Aspect.aspect targetAspect;
	protected Team.team targetTeam;
	protected bool targetAlive;

	/*
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	}
	*/

	protected bool assertPerception(Aspect al){
		return (al && al.aspectAct == targetAspect && al.teamAct == targetTeam && al.alive == targetAlive);
	}

}
