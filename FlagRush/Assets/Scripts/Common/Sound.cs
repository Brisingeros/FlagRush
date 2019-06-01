using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : Aspect {

	private float elapsedTime;

	// Use this for initialization
	void Start () {
		elapsedTime = 3.0f;
		aspectAct = aspect.Sound;
	}
	
	// Update is called once per frame
	void Update () {
		float changeTime = Time.deltaTime;
		elapsedTime -= changeTime;

		transform.Rotate (0, 55*changeTime, 0);

		if (elapsedTime <= 0.0f)
			Destroy(gameObject);
	}
}
