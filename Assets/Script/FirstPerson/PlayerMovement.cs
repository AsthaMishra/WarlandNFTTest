using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform Orientation;
    public float movementSpeed;
    public float groubdDrag;

    public float playerHeight;
    public LayerMask isGround;
    bool isGrounded;

    public float jumpSpeed;
    public float jumCoolDown;
    public float airMultiplier;
    bool canJump  = true;

    float horizontalInput;
    float verticalInput;

    Rigidbody rb;
    Vector3 playerDir;

    public PlayerMultiplayer playerMultiplayer;
    PhotonView pView;

    private void OnEnable()
    {
        pView = GetComponent<PhotonView>();
        playerMultiplayer.HandleMultiPlayerObjectState(pView.IsMine);
        playerMultiplayer.player = pView.Controller;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!pView.IsMine)
            return;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);

        if (isGrounded)
        {
            rb.drag = groubdDrag;
        }
        else
        {
            rb.drag = 0;
        }
        PlayerInput();
        ControlSpeed();


    }

    private void FixedUpdate()
    {
        if (!pView.IsMine)
            return;

        MovePlayer();
    }

    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        if (Input.GetKeyDown(KeyCode.Space) && canJump && isGrounded)
        {
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumCoolDown);
        }
    }

    void MovePlayer()
    {
        //player direction
        playerDir = Orientation.right * horizontalInput + Orientation.forward * verticalInput;

        if (isGrounded)
            rb.AddForce(playerDir.normalized * movementSpeed * 10f, ForceMode.Force);
        else if (!isGrounded)
            rb.AddForce(playerDir.normalized * movementSpeed * 10f * airMultiplier, ForceMode.Force);

    }

    void ControlSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
    }

    void ResetJump()
    {
        canJump = true;
    }
}
