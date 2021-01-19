using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GazeManager : MonoBehaviour
{
   
    public GameManager gameManager;               //Script GameManager

    public GameObject sign_GameStart;            //Schriftzug des StartButtons
    public GameObject sign_Direction;            //Schriftzug des RichtungauswählenButtons
    public GameObject planeDirection;            //Button Richtungauswählen 
    private GameObject pushElement;              //Objekt für dem Button der ausgwaählt wird
    private GameObject teleportPoint;            //Objekt für das Auge ,welches ausgewählt wurde
    private GameObject[] teleportTag;            // Array für das Tag teloportTag

    public Transform cam;                        //Transformeigenschatften der Kamera
    public Transform golfball;                   //Transformeigenschatften des Golfballs

    private Renderer rend;                       // Damit man später den Meshrenderer Deaktivieren kann
    //Grüner Kreis als Zeitanzeige
    public Image img;                            // Grüner Kreis
    
    public float duration;                      // Zeitgrenze des grünen Kreises
    private float timer;
    
    private bool isgaze;                       // Wird Aktiviert, wenn man über etwas Hovert
    private bool buttonTag = false;            // Wird Aktiviert, wenn man ein Button ausgewählt hat

    private Vector3 startPosition;             // Speichert die Startposition des X-Rig
      
    void Start()
    {
        startPosition = transform.position;     // Speichert die Startposition des X-Rig

        // Array für das Tag teloportTag wir initialisiert
        teleportTag = GameObject.FindGameObjectsWithTag("TeleportTag");

        // telportTag werden ausgeschaltet
        foreach (var item in teleportTag)
        {
            item.SetActive(false);
        }
    }

    void Update()
    {
        //Wenn mit dem Recticle gehovert wird
        if (isgaze)
        {
            //Solange der Timer kleiner als die Zeitgrenze ist wird der Kreis grüne Kreis gefüllt
            if (duration > timer)
            {
                timer += Time.deltaTime;
                img.fillAmount = timer / duration;
            }
            else
            {
                // Wenn ein Button ausgewählt wurde
                if (buttonTag)
                {
                    //Der Renderer des PushElements wird initalisiert und auf false gesetzt
                    //Den Button konnten wir nicht dierekt auf SetActiv(false) setzen,
                    //da es immer Fehlermeldunghen gab
                    rend = pushElement.GetComponent<Renderer>();
                    rend.enabled = false;

                    //Wenn der Startbutton ausgewählt wurde verschwindet auch der Schriftzug
                    // und die Augen werden angeschaltet
                    //Die Methode StartGameManager wird aktiviert
                    if (pushElement.name == "PlaneStartGame" && buttonTag)
                    {
                        sign_GameStart.SetActive(false);
                        gameManager.StartGame();

                        foreach (var item in teleportTag)
                        {
                            item.SetActive(true);
                        }
                    }
                    //Wenn der Richtungsauswahlbutton ausgewählt wurde verschwindet auch der Schriftzug
                    //Und im Gamemangaer start die Richtunsauswahlphase
                    if (pushElement.name == "PlaneDirection")
                    {
                        sign_Direction.SetActive(false);
                        gameManager.DirectionPhase();
                    }

                    buttonTag = false;
                }
                // Wenn kein Button ausgewählt wurde wird Teleportieren aktiviert
                else
                {
                    TeleportInteraction(teleportPoint);
                }

                timer = 0;

                isgaze = false;
                //Füllfarbe wird wieder auf 0 gestzt
                img.fillAmount = 0;
            }
        }
        // Wenn der Boolean overTeleTime auf True gesetzt wurde, wird die Teleportphase aktiviert
        if(gameManager.overTeleTime)
        {
            gameManager.StartTelePhase();
        }
       // Nach der Teleportphase werden die Augen wieder deaktiviert
       if(gameManager.overTelePhase())
       {
           transform.position = startPosition;
           foreach (var item in teleportTag)
           {
               item.SetActive(false);
           }
            //Startbutton wird auf False gesetzt dies funktioniert nicht in der Updatmethode,
            // da sonst es zu einer Fehlermeldung kommt
            pushElement.SetActive(false);
            //Richtungsauswahl-Button wird aktiviert
            planeDirection.SetActive(true);
            //Kamera schaut richtung Ball
            SeeOnBall();
       }
    }

    // Methode setzt Grünen Kreis auf 0 wenn man nicht mehr Rüberhovert
    public void HoverOff()
    {
        if (isgaze)
        {
            img.fillAmount = 0;
            isgaze = false;

        }
    }

    //Wenn man über ein Object hovert und es das Script CarboardInteracteble besitzt bekommt es darüber 
    //den Teleportpoint
    public void StartCount(GameObject teleportPoint)
    {
        //Position vom gehoverten Teleport wird übergeben
        this.teleportPoint = teleportPoint;
        // Weil gehovert wird ist der Boolean true
        isgaze = true;

    }

    //Wenn man über ein Button hovert und es das Script CarboardInteracteble besitzt bekommt es darüber 
    //den Button
    public void PushButton(GameObject button)
    {
        pushElement = button;
        isgaze = true;
        //Signalisier, das ein Button ausgewählt wurde
        buttonTag = true;

    }
    // Löst die Methode Telpeport aus  und bekommt den teleport überschrieben
    public void TeleportInteraction(GameObject teleportPoint)
    {   
    
        this.teleportPoint = teleportPoint;
        Teleport();
    }

     // Die Methode setzt die Position des XR-Rigs auf den Teleportpoint um 
    private void Teleport()
    {
        transform.position = teleportPoint.transform.position;
        //XR-Rig schaut auf den Ball
        SeeOnBall();
    }

    //Die Methode sorgt dafür, das das XR-Rig auf den Ball schaut
    private void SeeOnBall()
    {
        // Dreht das GameObjekt so, dass der Vorwärtsvektor auf die Ballposition zeigt
        //LookAt kann nur Transform verarbeiten 
        cam.transform.LookAt(golfball);
    }

}
