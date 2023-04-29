using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McMovement : MonoBehaviour
{

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

    public void TakeDamage(int damage)
    {
        print("El personaje recibio: " + damage);
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
        }
        else // If the player is in the air
        {
            // Apply movement velocity in the direction the player is facing
            Vector3 forwardMove = transform.forward * speed * Time.deltaTime;
            myRig.velocity = new Vector3(forwardMove.x, myRig.velocity.y, forwardMove.z);
        }
    }
}
