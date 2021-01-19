using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour
{
    private float x;
    private float y;
    private Vector3 rotateValue;

    void Update()
    {
        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        //Debug.Log(x + ":" + y);
        rotateValue = new Vector3(x, y * -1, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;
    }
}