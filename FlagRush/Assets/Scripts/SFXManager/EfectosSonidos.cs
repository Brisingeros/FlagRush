using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectosSonidos: MonoBehaviour {

    public AudioClip disparoSonido;
    public AudioClip andarSonidoIzq;
    public AudioClip andarSonidoDrch;
    public AudioClip muerteSonido;
    public AudioClip alertaSonido;
    public AudioClip resucitarSonido;
    public AudioClip meleeSonido;
    public AudioClip maniobraResucitar;
    public AudioSource audioSource;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void melee()
    {

        audioSource.PlayOneShot(meleeSonido);


    }


    void disparo()
    {

        audioSource.PlayOneShot(disparoSonido);

    }

    void andar_izq()
    {

        audioSource.PlayOneShot(andarSonidoIzq);

    }

    void andar_drch()
    {

        audioSource.PlayOneShot(andarSonidoDrch);

    }

    void muerte()
    {

        audioSource.PlayOneShot(muerteSonido);

    }


    void alerta()
    {



    }

    void resucitar()
    {

        audioSource.PlayOneShot(resucitarSonido);

    }

    void revivir()
    {
        audioSource.PlayOneShot(maniobraResucitar);
    }
}
