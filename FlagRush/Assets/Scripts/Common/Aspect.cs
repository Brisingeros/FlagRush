using System.Collections.Generic;
using UnityEngine;

public class Aspect : MonoBehaviour {

	public enum aspect
	{
		Sound,
		NPC
	}

	public aspect aspectAct;
	public Team.team teamAct;
	public bool alive;
}
