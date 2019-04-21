using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionBarricade : MonoBehaviour {

	public GameObject marker;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 getMarker(){
		return marker.transform.position;
	}
}
