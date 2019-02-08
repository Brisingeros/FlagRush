using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Player {
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("Alerta", enemiesSound.Count > 0);
		anim.SetBool("Enemigo", focus != null);
	}
}
