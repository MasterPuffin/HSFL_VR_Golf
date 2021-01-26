using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTemplateProjects;

public class GameManager : MonoBehaviour
{
    public float arrowConfirmTime = 10;             // die Zeit bis der Pfeil verschwindet und die Richtung festgelegt wird 
    bool boolV = true;
    bool startDirection = false;
    public float teleportTime = 25;                 // Zeit zum anschauen der Map, also die Zeit die man in der Teleportphase ist
    public bool overTeleTime = false;
    public GameObject hudTeleport;
    public GameObject hudGolf;
    
    public void DirectionPhase()
    {
        startDirection = true;                          // Setzt StartDirection auf True
    }


    public bool aimPhase()                        // gibt zurück ob man in der Aimphase ist (Richtungsauswahl)
    {
        if(arrowConfirmTime >= 0 && startDirection)     // wenn die Arrowconfirmtime größer als Null ist und startdirection ist True
        {
            hudGolf.SetActive(true);                    // das GolfHUD wird aktiviert
            boolV = true;                               // Boolien wird auf true gesetzt
            aimTime();                                  //Aimtime wird ausgeführt
            return true;                                //True wird zurück gelifert
        }
        else
        {
            hudGolf.SetActive(false);                   // das GolfHUD wird deaktiviert
            return false;                               // False wird zurück geliefert
        }
    }


    public bool overAimPhase(){                         
        return arrowConfirmTime <= 0;                   // gibt true zurück wenn kleiner gleich null ist, sonst false
    }

    public void aimTime()                               // Hier wird die Zeit runter gezählt
    { 
       arrowConfirmTime -= Time.deltaTime /2;           // Durch 2 weil wir die funktion 2 mal aufrufen...
    }   

    public void StartGame()  
    {
     overTeleTime = true;                               // Setzt OverTeleTime auf True
    } 

    public void StartTelePhase()
    {
        hudTeleport.SetActive(true);                    // TeleportHud wird true gesetzt
        teleportTime -= Time.deltaTime;                 // zählt Teleportzeit zurück
    }

    public bool rotationRXRig()
    {
        if (overAimPhase() && boolV)                    // wenn OverAimphase True ist und der BoolV auch
        {
            boolV = false;                              // boolV wird wieder false gesetzt
            return true;                                // True wird zurück gelifert
        }
        else
        {
            return false;                               // False wird zurück gelifert
        }
    }

    public bool overTelePhase()     
    {
        if(teleportTime <= 0 && overTeleTime)            // wenn die Teleportzeit kleiner gleich Null ist und Overteletime true ist
        {
            overTeleTime =false;                          // Wird overteletime auf False gestzt
            hudTeleport.SetActive(false);                 // Teleport HUD wird auf True gesetzt
            return true;                                  // True werd zurück gegeben
        }
        else
        {
            return false;                                   // False wird zurück gelifert
        }
    }

    //Funktionen die noch nicht implementiert wurden
    public void Win()
    {

    }

    public void Lose()
    {

    }
}
