using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCamController : MonoBehaviour
{

    public float sensX = 200;
    public float sensY = 200;

    public Transform orientation;

    float xRotation;
    float yRotation;

    float xRotationMin = -30f;
    float xRotationMax = 30f;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX;
        xRotation -= mouseY;


        xRotation = Mathf.Clamp(xRotation, xRotationMin, xRotationMax);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
