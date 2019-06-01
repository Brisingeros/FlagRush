using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGenerator : MonoBehaviour {

	public GameObject[] bubbles;
	public Material[] colors;

	public GameObject Initialize(Vector3 pos, Team.team tm, bool al) {
		GameObject soundObject = Instantiate ((al) ? bubbles[0] : bubbles[1]);

		Sound soundScript = soundObject.GetComponent<Sound> ();
		Renderer soundRenderer = soundObject.GetComponent<Renderer> ();

		soundScript.teamAct = tm;
		soundScript.alive = al;

		float sumHeight = (al) ? 5.0f : 2.5f;
		soundObject.transform.position = new Vector3(pos.x, pos.y + sumHeight, pos.z);

		int actMaterial = (tm == Team.team.Blue) ? 0 : 2;
		if (!al)
			actMaterial++;
		
		soundRenderer.sharedMaterial = colors[actMaterial];
		soundRenderer.enabled = true;

		return soundObject;
	}
}
