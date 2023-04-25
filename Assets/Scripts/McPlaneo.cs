using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McPlaneo : MonoBehaviour
{
    public TrailRenderer glidy;

    public float forwardSpeed = 5f;
    public float descentSpeed = 2f;
    public float glideDuration = 1.5f;
    public float glideGravity = 2f;

    private Rigidbody rb;
    private float glideTime = 0f;
    private bool isGliding = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        glidy = GetComponent<TrailRenderer>();
        glidy.enabled = false;
    }

    private void Update()
    {
        // Comprobar si se presiona la tecla G y si el personaje no está planeando actualmente
        if (Input.GetKeyDown(KeyCode.G) && !isGliding)
        {
            isGliding = true;
            glideTime = glideDuration;
            rb.useGravity = false;
        }
        // Comprobar si se presiona la tecla G y si el personaje está planeando actualmente
        else if (Input.GetKeyDown(KeyCode.G) && isGliding)
        {
            isGliding = false;
            glideTime = 0f;
            rb.useGravity = true;
            glidy.Clear();
            glidy.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (isGliding && glideTime > 0f)
        {
            glideTime -= Time.deltaTime;
            rb.velocity = transform.forward * forwardSpeed + Vector3.down * descentSpeed;
            rb.AddForce(Vector3.down * glideGravity, ForceMode.Acceleration);

            glidy.enabled = true;
        }
        else
        {
            isGliding = false;
            rb.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGliding = false;
            glideTime = 0f;
            glidy.Clear();
            glidy.enabled = false;
        }
    }
}
