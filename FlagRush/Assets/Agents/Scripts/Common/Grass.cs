using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Player pl = other.GetComponent<Player>();
        if (pl)
        {
            pl.setHidden(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
