using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemy : MonoBehaviour
{

    public int damage;
    public NavMeshAgent agent;
    //private Rigidbody rb;
    private Transform ActualWaypoint;
    public float grado;
    private Transform FollowingWaypoint;

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
        //rb = GetComponent<Rigidbody>();
        

    }

    private void Update()
    {
        animator.SetBool("run", false);
        PlayerDetection = Physics.CheckSphere(transform.position, radioView, PlayerCape);
        
        if(PlayerDetection == true) 
        {
            
             

            transform.LookAt(GameManager.instance.player.transform.position);
             
            animator.SetBool("run", true);
            transform.position = Vector3.MoveTowards(transform.position, GameManager.instance.player.transform.position, speed * Time.deltaTime);
             
         
        }
        else
        {

            //var disToWaypoint = Vector3.Distance(FollowingWaypoint.position, transform.position);
            //agent.enabled = true;
            //animator.SetBool("walk", true);

           // if (disToWaypoint <= distToCheck)
            {

                //agent.SetDestination(ActualWaypoint.position);
               // print($"check{ActualWaypoint.name}.");
                          

            
             
            
                
            }
            



        }



    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioView);
    }
   


}







