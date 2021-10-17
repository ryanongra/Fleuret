using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Credits to Learn Everything Fast for this script (with slight modifications)
 * https://www.youtube.com/watch?v=lYIRm4QEqro
 */
public class CameraController : MonoBehaviour
{

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public float yaw = 0.0f;
    public float pitch = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Controlling yaw with constraints
        //if (yaw < -130)
        //{
        //    yaw = -130;
        //}
        //else if (yaw > -50)
        //{
        //    yaw = -50;
        //}
        //else
        //{
        //    yaw += speedH * Input.GetAxis("Mouse X");
        //}

        //// Controlling pitch with constraints
        //if (pitch < -13)
        //{
        //    pitch = -13;
        //}
        //else if (pitch > 13)
        //{
        //    pitch = 13;
        //}
        //else
        //{
        //    pitch -= speedV * Input.GetAxis("Mouse Y");
        //}

        //transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(Mathf.Clamp(pitch, -30, 20),
            Mathf.Clamp(yaw, -130, -50),
            0.0f);

        
    }
}
