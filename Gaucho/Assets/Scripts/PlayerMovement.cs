using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if(isGrounded && velocity.y < 0)
        {
            // to make sure player isn't floating b/c groundCheck radius
            velocity.y = -2f;
        }
        // player movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // uses local, instead of global axis (MouseLook changes local axis by rotating player)
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * 12f * Time.deltaTime);

        // jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime; // gravity = velocity * time^2
        controller.Move(velocity * Time.deltaTime);
        //Debug.Log(velocity.y);

    }
}
