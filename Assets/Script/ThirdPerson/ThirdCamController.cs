using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCamController : MonoBehaviour
{

    public Transform Orientation;
    public Transform playerObj;
    public Transform player;
    public Rigidbody playerRb;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDir = player.position - new Vector3(this.transform.position.x, player.position.y, this.transform.position.z);
        Orientation.forward = viewDir.normalized;


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDir = Orientation.forward * horizontal + Orientation.right * vertical;

        if (inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * speed);
        }
    }
}
