using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : Aspect {

	private float elapsedTime;

	// Use this for initialization
	void Start () {
		elapsedTime = 7.0f;
		aspectAct = aspect.Sound;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime -= Time.deltaTime;

		if (elapsedTime <= 0.0f)
			Destroy (gameObject);
	}
}
