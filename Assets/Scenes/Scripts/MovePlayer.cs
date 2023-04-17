using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  [RequireComponent(typeof(Rigidbody))]
 public class MovePlayer : MonoBehaviour
 {
   
    private Rigidbody rb;
 
   
    [Header("Values")]
    [SerializeField] private float Speed;

    public Animator PlayerAnimatorController;
    
    private float xAxis, zAxis;
    //public int damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerAnimatorController = GetComponent<Animator>(); // hola este es un comentari
        

    }
    private void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");
        PlayerAnimatorController.SetFloat("MoveZ",zAxis);
        PlayerAnimatorController.SetFloat("Move", xAxis);

        
    }
    private void FixedUpdate()
    {

        if (xAxis != 0 || zAxis != 0) 
        {
            Move(xAxis, zAxis);
           

        }
       

    }
    private void Move(float xAxis, float zAxis) 
    {
       var dir = (transform.right * xAxis + transform.forward * zAxis);
        
        rb.MovePosition(transform.position + dir * Speed * Time.fixedDeltaTime); 
    
    }
}
