using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityTemplateProjects;

public class BallController : MonoBehaviour {
    private Vector3 startPostion;
    public MoveSaver moveSaver; // Movesaver Script               
    public GameObject rotationCamera;
    public GameObject golfclub;
    public GameObject xrrig;
    private Rigidbody rb;
    public float ballForce = 5000f; // Ball stärke (wenn der Schlag zu doll ist hier ändern)
    public bool hit = false;
    private Vector3 v3Force;
    private Vector3 lastPosition;
    public AudioClip hitSound;
    public GameObject arrow;
    public GameManager gameManager;
    public GolfHoleScript golfHoleScript;
    private Vector3 pos;
    private bool onObstacle;
    private int holeCounter;

    // Start is called before the first frame update
    void Start () {
        startPostion = transform.position; // speichert die Anfangsposition des Balls
        arrow = GameObject.Find ("ArrowManager"); // weißt arrow dem ArrowManager Objekt zu
        rb = GetComponent<Rigidbody> (); // weißt die Rigidbody Komponente zu
        golfHoleScript.holeWon = false;
    }

    // Update is called once per frame
    void Update () {

        if (golfHoleScript.holeWon) // Wenn man ins Ziel getroffen hat
        {

            transform.position = nextPos (); // wird der Ball zur Startposition gesetzt
            startPostion = nextPos ();
            golfHoleScript.holeWon = false; // holeWon wird wieder auf false gesetzt
            arrow.transform.position = transform.position;
            Debug.Log ("Loch: " + holeCounter);
        }
        v3Force = -rotationCamera.transform.right; // v3Force bekommt die Blickrichtung der Kamera übergeben 
        v3Force.y = 0; // der Wert der Y-Achse muss auf 0 gesetzt werden damit die vertikale Drehung 
        // der Kamera keine Auswirkung auf die Kugel hat

        if (hit) //Wenn der Ball getroffen wurde
        {
            //if(rb.IsSleeping() && !golfHoleScript.holeWon)             // Wenn der Ball sich nicht mehr bewegt und nicht im Ziel ist
            if (rb.velocity.magnitude < 0.1 && !golfHoleScript.holeWon) // Wenn der Ball sich nicht mehr bewegt und nicht im Ziel ist
            {
                //Stop ball movement
                rb.velocity = new Vector3 (0, 0, 0);
                Debug.Log ("Bewegt sich nicht mehr");
                xrrig.transform.position = transform.position; // XR Rig wird auf die Position des Balls gesetzt
                xrrig.transform.Rotate (0, -90, 0); // XR Rig wird um 90 Grad zurück gedreht
                hit = false; // hit wird auf fals gesetzt
                golfclub.SetActive (true); // Golfschläger wird Sichtbar gemacht
                arrow.transform.position = transform.position; // Richtungspfeil wird aktiviert
                gameManager.arrowConfirmTime = 8; // ArrowConfirmTime wird auf 8 gesetzt

            }
        }
    }

    private void OnCollisionExit (Collision collision) {
        if (collision.gameObject.tag == "Obstacle") {
            onObstacle = false;
        }
    }

    public void OnCollisionEnter (Collision col) {

        if (col.gameObject.tag == "Obstacle") {
            onObstacle = true;
        }
        if (!hit) //Wenn der Ball noch nicht getroffen wurde
        {
            if (col.gameObject.tag == "Player") //Wenn der Golfschläger den Ball trifft
            {
                rb.AddForce (v3Force * moveSaver.BallForce () * ballForce); // Ballkraft wird auf den Ball angewendet
                AudioSource.PlayClipAtPoint (hitSound, transform.position, 0.7f); // Sound wird abgespielt
                golfclub.SetActive (false); // Schläger wird Deaktiviert
                lastPosition = transform.position; // letzte Position des Balls wird gespeichert
                hit = true; // hit wird auf true gesetzt damit man nur einmal schießen kann
            }
        }
        if (col.gameObject.tag == "OutOff") // Wenn der Ball den Ground trifft
        {
            transform.position = lastPosition; // wird er auf die Letzte Position zurück gebracht
            StopBall (); // Und die Funktionb Stopball() wird ausgeführt
        }
    }

    //Funktion die den Ball Stoppt so das er sich nach teleportation nicht mehr bewegt 
    public void StopBall () {
        rb.velocity = Vector3.zero; // Velocity wird auf null gesetzt
        rb.angularVelocity = Vector3.zero; // angular Velocity wird auch auf Null gesetzt
    }

    private Vector3 nextPos () {
        
        //hier sind alle startpositionen für den Ball in den einzelnen Bahnen gespeichert
        switch (golfHoleScript.holeCounter) {
            case 0:
                pos = new Vector3 (0.5134f, 0.1103f, -2.2452f);
                break;
            case 1:
                pos = new Vector3 (13.798f, 0.122f, -1.464f);
                break;
            case 2:
                pos = new Vector3 (-7.09f, 0.1103f, 5.685f);
                break;
            case 3:
                pos = new Vector3 (-10.31f, 0.1103f, 13.422f);
                break;
            case 4:
                pos = new Vector3 (-8.798f, 0.1103f, 15.985f);
                break;
            case 5:
                pos = new Vector3 (1.13f, 1.32f, 17.462f);
                break;
            case 6:
                pos = new Vector3 (5.441f, 0.122f, 4.912f);
                break;
            case 7:
                pos = new Vector3 (3.475f, 0.122f, -10.264f);
                break;
            case 8:
                pos = new Vector3 (-4.221f, 0.1103f, -0.05f);
                break;
        }

        return pos;
    }
}