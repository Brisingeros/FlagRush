using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {

    // num army
    private int redSoldiers = 1;
    private int blueSoldiers = 1;
    private int redSnipers = 1;
    private int blueSnipers = 1;
    private int redNurses = 1;
    private int blueNurses = 1;

    //const
    private const int MAX_SOLDIERS = 10;
    private const int MIN_SOLDIERS = 1;
    private const int MAX_SNIPERS = 10;
    private const int MIN_SNIPERS = 1;
    private const int MAX_NURSES = 10;
    private const int MIN_NURSES = 1;

    void Start () {

        if (FindObjectsOfType<Controller>().Length == 1)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
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
        SceneManager.LoadScene("SampleScene");
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
                else if (redSoldiers == MIN_SOLDIERS) uiController.setInteractableButton(type, "menos", true);

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
