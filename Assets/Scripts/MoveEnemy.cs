using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemy : MonoBehaviour
{

    public int damage;
    public NavMeshAgent agent;
    private Rigidbody rb;
    private Transform ActualWaypoint;
    public float grado;

    [Header("Distance")]
    [SerializeField] private float distToCheck;
    [Header("AI")] public List<Transform> Nods = new List<Transform>(); 
   
    public float speed;
    public float radioView;
    public float distAtack;
    
    
    public Animator animator;
    public LayerMask PlayerCape;
    public Quaternion angle;

  
    bool PlayerDetection;
    
    

    private void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();

        ActualWaypoint = Nods[Random.Range(0, Nods.Count)];
        agent.SetDestination(ActualWaypoint.position);
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        animator.SetBool("run", false);
        PlayerDetection = Physics.CheckSphere(transform.position, radioView, PlayerCape);
        
        if(PlayerDetection == true) 
        { 
             Vector3 PlayerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
             transform.LookAt(PlayerPosition);
             
            animator.SetBool("run", true);
            transform.position = Vector3.MoveTowards(transform.position, PlayerPosition, speed * Time.deltaTime);
             
         
        }
        else
        {

            var disToWaypoint = Vector3.Distance(ActualWaypoint.position, transform.position);
            animator.SetBool("walk", true);

            if (disToWaypoint <= distToCheck)
            {

                print($"check{ActualWaypoint.name}.");
                agent.SetDestination(ActualWaypoint.position);
                //Move_Enemy(ActualWaypoint);

            
             
            
                
            }
            if (ActualWaypoint == true) 
            {
                var NewNod = Nods[Random.Range(0, Nods.Count)];

            }



        }



    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioView);
    }
    //public void Move_Enemy(Transform actualWaypoint)
    //{

        

        

    //    grado = Random.Range(0, 360);
    //    angle = Quaternion.Euler(0, grado, 0);

    //    if (NewNod == actualWaypoint)
    //    {
    //        Move_Enemy(actualWaypoint);

    //    }
    //    else 
    //    {
    //        actualWaypoint = NewNod;

    //    } 


    //}


}







