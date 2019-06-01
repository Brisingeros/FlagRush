using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Vector3 size;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnPrefab(GameObject prefab, int num){
		Vector3 pos;
		Vector3 center = this.gameObject.transform.position;

		for (int i = 0; i < num; i++) {
			pos = center + new Vector3 (Random.Range (-size.x / 2, size.x / 2), 0.0f, Random.Range (-size.z / 2, size.z / 2));
			Instantiate (prefab, pos, Quaternion.identity);
		}
	}

	void OnDrawGizmosSelected () {
		Gizmos.color = new Color (1,0,0,0.5f);
		Gizmos.DrawCube (this.gameObject.transform.position, size);
	}
}
