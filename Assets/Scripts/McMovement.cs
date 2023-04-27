using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McMovement : MonoBehaviour
{
    public ParticleSystem dust;

    private Rigidbody myRig;
    private float yVelocity;
    public float speed = 200f;

    private JumpNoAsignado jumpy;

    private Transform playerTransform;

    private void Awake()
    {
        myRig = GetComponent<Rigidbody>();
        jumpy = GetComponent<JumpNoAsignado>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        dust.transform.position = playerTransform.position;
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Check if the player is on the ground
        if (jumpy.onFloor)
        {
            // Apply movement velocity only on the x and z axes
            Vector3 horizontalMove = moveDirection * speed * Time.deltaTime;
            myRig.velocity = new Vector3(horizontalMove.x, myRig.velocity.y, horizontalMove.z);

            // check if the player is moving and on the ground
            if (moveDirection.magnitude > 0)
            {
                // play the particle system
                if (!dust.isPlaying)
                {
                    dust.Play();
                }
            }
            else
            {
                // stop the particle system
                if (dust.isPlaying)
                {
                    dust.Stop();
                }
            }
        }
        else // If the player is in the air
        {
            // Apply movement velocity in the direction the player is facing
            Vector3 forwardMove = transform.forward * speed * Time.deltaTime;
            myRig.velocity = new Vector3(forwardMove.x, myRig.velocity.y, forwardMove.z);
        }
    }
}
