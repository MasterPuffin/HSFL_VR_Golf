using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityTemplateProjects;
using System.Security.Cryptography;
using UnityTemplateProjects;

public class AimTimeFieldController : MonoBehaviour
{
    Text textComp;                                  //Textfeld
    public GameObject hudGolf;                      //GameObject des Textfeldes 
    public GameManager gameManager;                 //Script GameManager

    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<Text>();        //Textfeld wird initialisiert
        textComp.text = ShowTime();             //greift auf das textfeld zu
    }

    // Update is called once per frame
    private void Update()
    {
        //wenn die Aimphase beginnt also die Richtungsauswahl, wirs das hud aktiviert und die startet Showtime
        //wenn die Aimphase vorbei ist wir das HUD wieder deaktiviert
        if (gameManager.aimPhase())
        {
            hudGolf.SetActive(true);
            textComp.text = ShowTime() + "sec";
        }
        else
        {
            hudGolf.SetActive(false);
        }
    }

    //Greift auf die Arrowconfirmtime im Gamemananger zu
    public string ShowTime()
    {
        return gameManager.arrowConfirmTime.ToString("F2");

    }
}