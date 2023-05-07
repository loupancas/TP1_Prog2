using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McSalto : MonoBehaviour
{
    public ParticleSystem dustJump;
    public ParticleSystem inicioParticles;
    public ParticleSystem medioParticles;
    public ParticleSystem finParticles;
    private Transform playerTransform;

    Rigidbody myRig;
    public float jumpForce = 5;
    public bool onFloor = true;
    public int jumpsLeft = 2;

    void Awake()
    {
        jumpsLeft = 2;
        myRig = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onFloor)
        {

            if (onFloor)
            {
                myRig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                
                jumpsLeft = 2;
                inicioParticles.transform.position = playerTransform.position;
                inicioParticles.Play();
                //dustJump.Play();
            }
            else if (jumpsLeft == 2)
            {
                myRig.velocity = new Vector3(myRig.velocity.x, 0f, myRig.velocity.z);
                myRig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpsLeft--;
                medioParticles.transform.position = playerTransform.position;
                medioParticles.Play();
                //dustJump.Play();
            }
            else if (jumpsLeft == 1)
            {
                myRig.velocity = new Vector3(myRig.velocity.x, 0f, myRig.velocity.z);
                myRig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpsLeft--;
                finParticles.transform.position = playerTransform.position;
                finParticles.Play();
                //dustJump.Play();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = true;
            jumpsLeft = 2;
            dustJump.transform.position = playerTransform.position;
            dustJump.Play();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = false;
        }
    }
}
