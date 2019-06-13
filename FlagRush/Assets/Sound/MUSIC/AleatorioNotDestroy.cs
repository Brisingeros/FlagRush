using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AleatorioNotDestroy : MonoBehaviour {

    public AudioSource bckgMusic1;
    public AudioSource bckgMusic2;
    public AudioSource bckgMusic3;

    int aleatorio;


    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start () {

        aleatoriedad();
        if (aleatorio == 1)
        {

            bckgMusic1.Play();
            bckgMusic2.Stop();
            bckgMusic3.Stop();



        }
        else if (aleatorio == 2)
        {

            bckgMusic1.Stop();
            bckgMusic2.Play();
            bckgMusic3.Stop();

        }
        else if (aleatorio == 3)
        {

            bckgMusic1.Stop();
            bckgMusic2.Stop();
            bckgMusic3.Play();

        }


    }
	
	// Update is called once per frame
	void Update () {
		
        


    }

    public void aleatoriedad()
    {
        aleatorio = Random.Range(1, 4);
   
    }
}
