using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GolfHoleScript : MonoBehaviour {
    public AudioClip sound;
    public BallController ballcontroller;
    public GameObject planeWin;
    public bool holeWon;
    public float winTime = 10;
    public float timer = 10;
    private bool holeTrigger = false;
    public int holeCounter;

    //Hier Funktioniert der Timer und das erscheinen der WinPlane noch nicht, haben es trotzdem für euch drinnen gelassen
    void Update () {
        if (!holeWon) //Wenn der Ball noch nicht im Ziel ist
        {
            planeWin.SetActive (false); //bleibt die Gewinnerplane deaktiviert
        }

        if (holeTrigger) // wenn der Ball ins Ziel trifft
        {

            holeWon = true;
            holeTrigger = false;
            holeCounter += 1;
            if (holeCounter == 9) {
                holeCounter = 0;
            }
            Debug.Log("LochCount: "+holeCounter);

        }
    }

    void OnTriggerEnter (Collider other) {
        if (other.name == "Golfball") { //wenn der Ball in das Loch fällt
            planeWin.SetActive (true); //setzt die Win Plane true
            AudioSource.PlayClipAtPoint (sound, transform.position, 0.7f); //Spiel Sound ab
            holeTrigger = true; //setzte holeTrigger auf true
            ballcontroller.StopBall (); //führe die funktion stopball() aus dem BallController aus
        }
    }
}