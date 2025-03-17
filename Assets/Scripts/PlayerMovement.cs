using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public int sprint;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f,0f,0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // Resting the default velocity
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Get the inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Creating the moving vector
        Vector3 move = transform.right * x + transform.forward * z;


        // add sprint 
        if(Input.GetKey(KeyCode.LeftShift))
        {
        // Actually moving
        controller.Move(speed * Time.deltaTime * move * sprint);
        }
        else
        {
        // Actually moving
        controller.Move(speed * Time.deltaTime * move);
        }


        // Check if Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            // jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Check if Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            // jump
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        // Falling down
        velocity.y += gravity * Time.deltaTime;

        // Executing Jump
        controller.Move(velocity * Time.deltaTime);

        if(lastPosition != gameObject.transform.position && isGrounded == true)
        {
            isMoving = true;
            // for last use
        }
        else
        {
            isMoving = false;
            // for later use 
        }

        lastPosition = gameObject.transform.position;

    }
}
