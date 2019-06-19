using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{

    public Terrain m_terrain;  //referencia al terreno
    public float DrawDistance; //distancia de visibilidad, poner a 5000 en el editor

    // Use this for initialization
    void Start()
    {

        m_terrain.detailObjectDistance = DrawDistance;

    }

    void update()
    {

        m_terrain.detailObjectDistance = DrawDistance;

    }
}
