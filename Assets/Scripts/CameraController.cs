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

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(Mathf.Clamp(pitch, -30, 20),
            Mathf.Clamp(yaw, -130, -50),
            0.0f);

        
    }

    void LateUpdate()
    {
        /*Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, smoothSpeed);*/
        transform.position = target.position + offset;
    }
}
