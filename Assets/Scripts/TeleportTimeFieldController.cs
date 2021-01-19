using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;
using System.Security.Cryptography;
using UnityTemplateProjects;

public class TeleportTimeFieldController : MonoBehaviour
{
    Text textComp;                          //Textfeld
    public GameManager gameManager;         //Script GameManager

    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<Text>();        //Textfeld wird initialisiert
        textComp.text = ShowTime();             //greift auf das textfeld uz
    }

    // Update is called once per frame


    private void Update()
    {
        //Wenn die overTelephase false ist (also die Teleportphase noch nicht vorbei ist) wird die Zeit angezeigt
        if (!gameManager.overTelePhase())
        {
            textComp.text = ShowTime() + "sec";
        }
    }

    public string ShowTime()
    {
        //F2 steht für die nachkommerstellen des Floats
        return gameManager.teleportTime.ToString("F2");

    }
}