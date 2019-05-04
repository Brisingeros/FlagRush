using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private void Start()
    {
    
        if(FindObjectsOfType<UIController>().Length == 1)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void setValueOnScreen(string tag, int value)
    {

        GameObject parent = GameObject.FindGameObjectWithTag(tag);
        GameObject number = parent.transform.Find("numero").gameObject;
        number.GetComponent<Text>().text = value.ToString();

    }

    public void setInteractableButton(string tag, string type, bool state)
    {
        GameObject parent = GameObject.FindGameObjectWithTag(tag);
        Button button = parent.transform.Find(type).gameObject.GetComponent<Button>();
        if(button.interactable != state)
            button.interactable = state;
    }
}
