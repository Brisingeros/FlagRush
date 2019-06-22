using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    //const
    private const int MAX_SOLDIERS = 9;
    private const int MIN_SOLDIERS = 1;
    private const int MAX_SNIPERS = 9;
    private const int MIN_SNIPERS = 0;
    private const int MAX_NURSES = 6;
    private const int MIN_NURSES = 0;


    // num army
    private int redSoldiers = 6;
    private int blueSoldiers = 6;
    private int redSnipers = 3;
    private int blueSnipers = 3;
    private int redNurses = 3;
    private int blueNurses = 3;

    void Start () {

        initializeValues("red team");
        initializeValues("blue team");

        if (FindObjectsOfType<Controller>().Length == 1)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

	}

    private void initializeValues(string team)
    {
        int initSol;
        int initNur;
        int initSnp;

        if (team.Equals("red team"))
        {
            initSol = redSoldiers;
            initNur = redNurses;
            initSnp = redSnipers;
        } else
        {
            initSol = blueSoldiers;
            initNur = blueNurses;
            initSnp = blueSnipers;
        }

        GameObject gO = GameObject.Find(team);
        for (int i = 0; i < gO.transform.childCount; i++)
        {
            GameObject child = gO.transform.GetChild(i).gameObject;
            if (child.transform.Find("numero"))
            {
                Text text = child.transform.Find("numero").GetComponent<Text>();

                if (child.name.Contains("soldiers"))
                {
                    text.text = initSol.ToString();
                }
                else if (child.name.Contains("snipers"))
                {
                    text.text = initNur.ToString();
                }
                else if (child.name.Contains("nurses"))
                {
                    text.text = initSnp.ToString();
                }
            }
        }

    }

    public void exit()
    {
        #if UNITY_EDITOR
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #else
		{
		    Application.Quit ();
		}
        #endif
    }

    public void simulate()
    {
        SceneManager.LoadScene("Game");
    }

    public void IncrementValue(string type)
    {

        UIController uiController = FindObjectOfType<UIController>();
        int newValue = 0;

        switch (type)
        {

            case "red soldiers": 
                newValue = ++redSoldiers;
                if (redSoldiers == MAX_SOLDIERS) uiController.setInteractableButton(type, "mas", false);
                else if (redSoldiers > MIN_SOLDIERS) uiController.setInteractableButton(type, "menos", true);

                break;
            case "blue soldiers": 
                newValue = ++blueSoldiers;
                if (blueSoldiers == MAX_SOLDIERS) uiController.setInteractableButton(type, "mas", false);
                else if (blueSoldiers > MIN_SOLDIERS) uiController.setInteractableButton(type, "menos", true);

                break;
            case "red snipers":
                newValue = ++redSnipers;
                if (redSnipers == MAX_SNIPERS) uiController.setInteractableButton(type, "mas", false);
                else if (redSnipers > MIN_SNIPERS) uiController.setInteractableButton(type, "menos", true);

                break;
            case "blue snipers":
                newValue = ++blueSnipers;
                if (blueSnipers == MAX_SNIPERS) uiController.setInteractableButton(type, "mas", false);
                else if (blueSnipers > MIN_SNIPERS) uiController.setInteractableButton(type, "menos", true);

                break;
            case "red nurses":
                newValue = ++redNurses;
                if (redNurses == MAX_NURSES) uiController.setInteractableButton(type, "mas", false);
                else if (redNurses > MIN_NURSES) uiController.setInteractableButton(type, "menos", true);

                break;
            case "blue nurses":
                newValue = ++blueNurses;
                if (blueNurses == MAX_NURSES) uiController.setInteractableButton(type, "mas", false);
                else if (blueNurses > MIN_NURSES) uiController.setInteractableButton(type, "menos", true);

                break;

        }

        uiController.setValueOnScreen(type, newValue);

    }

    public void DecrementValue(string type)
    {

        UIController uiController = FindObjectOfType<UIController>();
        int newValue = 0;

        switch (type)
        {
            case "red soldiers":
                newValue = --redSoldiers;
                if (redSoldiers == MIN_SOLDIERS) uiController.setInteractableButton(type, "menos", false);
                else if (redSoldiers < MAX_SOLDIERS) uiController.setInteractableButton(type, "mas", true);
                break;
            case "blue soldiers":
                newValue = --blueSoldiers;
                if (blueSoldiers == MIN_SOLDIERS) uiController.setInteractableButton(type, "menos", false);
                else if (blueSoldiers < MAX_SOLDIERS) uiController.setInteractableButton(type, "mas", true);
                break;
            case "red snipers":
                newValue = --redSnipers;
                if (redSnipers == MIN_SNIPERS) uiController.setInteractableButton(type, "menos", false);
                else if (redSnipers < MAX_SNIPERS) uiController.setInteractableButton(type, "mas", true);
                break;
            case "blue snipers":
                newValue = --blueSnipers;
                if (blueSnipers == MIN_SNIPERS) uiController.setInteractableButton(type, "menos", false);
                else if (blueSnipers < MAX_SNIPERS) uiController.setInteractableButton(type, "mas", true);
                break;
            case "red nurses":
                newValue = --redNurses;
                if (redNurses == MIN_NURSES) uiController.setInteractableButton(type, "menos", false);
                else if (redNurses < MAX_NURSES) uiController.setInteractableButton(type, "mas", true);
                break;
            case "blue nurses":
                newValue = --blueNurses;
                if (blueNurses == MIN_NURSES) uiController.setInteractableButton(type, "menos", false);
                else if (blueNurses < MAX_NURSES) uiController.setInteractableButton(type, "mas", true);
                break;
        }

        uiController.setValueOnScreen(type, newValue);

    }

    public int getRedSoldiers()
    {
        return redSoldiers;
    }

    public int getBlueSoldiers()
    {
        return blueSoldiers;
    }

    public int getRedSnipers()
    {
        return redSnipers;
    }

    public int getBlueSnipers()
    {
        return blueSnipers;
    }

    public int getRedNurses()
    {
        return redNurses;
    }

    public int getBlueNurses()
    {
        return blueNurses;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
