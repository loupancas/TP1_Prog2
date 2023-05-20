using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class McMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
   
   //public GameObject floor; 
    public CharacterController controller;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam; 
    private Vector2 _input;
    public float gravity=-9.81f;
    //public float gravityJump=-7f;
      bool isGrounded;
    public Transform groundCheck;
       public LayerMask groundMask;
      public float sphereRadius=0.3f;
   private float _currentVelocity;
   private Vector3 moveDir;
   public float timer=0.8f;
  private bool jumping=true;
   
   // public float jumpHeight = 1.5f;
    Charview view;
    private Vector3 movement;
    Vector3 velocity;
     [SerializeField] private float jumpPower= 15f;
    [SerializeField] private float gravityMultiplier = 3f;
    //[SerializeField] private ParticleSystem particulas;


    private void Awake()
    {
        view = GetComponent<Charview>();
        controller = GetComponent<CharacterController>();
    }



    void Start(){

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {   
        isGrounded = Physics.CheckSphere(groundCheck.position,sphereRadius,groundMask);

        ApplyGravity();
        ApplyRotation();
        ApplyMovement();

        if (Input.GetKeyDown(KeyCode.Q)) // animacion agarrar objetos
        {
            view.Grab();
        }

        view.horizontal(Input.GetAxis("Horizontal"));//animacion movimiento
        view.vertical(Input.GetAxis("Vertical"));//animacion movimiento

    }

    private void ApplyGravity(){
    if ( isGrounded == true && velocity.y < 0.0f)
        {
            velocity.y = -1.0f;
        }
        else{
        velocity.y += gravity*gravityMultiplier*Time.deltaTime;
        }
        velocity.y += gravity*Time.deltaTime;
        controller.Move(velocity*Time.deltaTime);
    }

    private void ApplyRotation(){
        if(_input.sqrMagnitude ==0) return;

        if(_input.sqrMagnitude >= 0.1f){
            
            float targetAngle = Mathf.Atan2(movement.x, movement.z)* Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle= Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle, ref turnSmoothVelocity, turnSmoothTime);
             moveDir = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f,angle,0f);
           controller.Move(moveDir.normalized*moveSpeed*Time.deltaTime); 
            
        }
        

        //view.horizontal(Input.GetAxis("Horizontal"));
      //  view.vertical(Input.GetAxis("Vertical"));

      /*  if (Input.GetKeyDown(KeyCode.Q))
        {
            view.Grab();
        }*/
   }

   private void ApplyMovement(){
    
    transform.Translate(movement * moveSpeed * Time.deltaTime);

   }

   public void Move(InputAction.CallbackContext context){
        
        _input = context.ReadValue<Vector2>();
        movement = new Vector3(_input.x, 0f, _input.y).normalized;
        
    }
   
    public void Jump(InputAction.CallbackContext context)
    {    
        
        jumpPower=10;
        if (!context.started) return;
        if (!isGrounded) return;
      if(jumping) {
        view.Jump();
        StartCoroutine("Wait");
      }

           
           
            
       
       
    }
    IEnumerator Wait(){
        jumping=false;

        yield return new WaitForSeconds (timer);
          velocity.y += jumpPower;
        yield return new WaitForSeconds (timer);
          jumping=true;
                 
    }

        
}
