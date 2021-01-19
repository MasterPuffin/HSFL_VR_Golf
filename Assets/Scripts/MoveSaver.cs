using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSaver : MonoBehaviour
{

    public Queue<Vector3> posis = new Queue<Vector3>();

    //größe der Que
    public int samples = 10;

    // Update is called once per frame
    void Update()
    {
        //fügt jedem Frame, die Position des Emptys, an Ende der Queue an
        posis.Enqueue(transform.position);

        //solange die Anzahl der Elemente in der Queue größer als samples ist, wird immer das erste Element (älteste) der Que entfernt
        while (posis.Count > samples)
        {
            posis.Dequeue();
        }
    }

//Funktion die die Kraft des Balls berrechnet
    public float BallForce()
    {
        //Speichert die que als Array
        Vector3[] moveSaverPositions = posis.ToArray();
        float min_velocity = 0f;
        
        //geht das Array bis zum vorletzten objekt durch
         for(int i = 0; i < moveSaverPositions.Length-1 ; i++)
        {
            //rechnet die aktuelle Position minus die nächste
            Vector3 _velocity = moveSaverPositions[i] - moveSaverPositions[i+1];

            //speichert den Wert in ein Globale Variable und addiert ihn auf
            min_velocity += _velocity.magnitude;
        }

        //rechnet den Durchschnittswert aus
        min_velocity = min_velocity / (moveSaverPositions.Length-1);

        return min_velocity;
    }




}
