using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sense : MonoBehaviour {

	public Player pla;

	// Use this for initialization
	void Start () {
		pla = this.transform.parent.GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

}
