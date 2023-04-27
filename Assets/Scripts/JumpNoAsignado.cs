using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpNoAsignado : MonoBehaviour
{
    public ParticleSystem dustJump;

    Rigidbody myRig;
    public float jumpForce = 5;
    public bool onFloor = true;

    //public bool saltoSinBug;

    private void Awake()
    {
        myRig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onFloor) // Agregar el  "saltoSinBug==true" si se usan los chekers
        {
            myRig.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            onFloor = false;
            dustJump.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onFloor = true;
            dustJump.Play();
        }
    }
}
