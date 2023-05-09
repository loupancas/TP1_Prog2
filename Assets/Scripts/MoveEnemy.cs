using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemy : MonoBehaviour
{

    public int damage;
    int CurrentNodsIndex;
    public NavMeshAgent agent;
   
    private Transform ActualWaypoint;
    
    public float ViewAngle = 90;
    public float StartTime = 4;
    public float StartTimeRotate = 2; 
    public float speed;
    public float radioView;
    public float distAtack;

    [Header("Distance")]
    [SerializeField] private float distToCheck;
    [Header("AI")] public Transform[] Nods;

    public Animator animator;
    public LayerMask PlayerCape;

    Vector3 PlayerLastPosition = Vector3.zero;
    Vector3 PlayerPosition;
    
    //bool IsPatrol;
    bool PlayerDetection;
    //bool PlayerNear;
    //bool PlayerInRange;
    //bool CaughtPlayer;
    //float TimeRotate;
    //float WaitTime;
    
    

    private void Start()
    {
        
        PlayerLastPosition = Vector3.zero;
        //IsPatrol = true;
        //CaughtPlayer = false;
        //PlayerInRange = false;
        //WaitTime = StartTime;
        //TimeRotate = StartTimeRotate;
        
        CurrentNodsIndex = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(Nods[CurrentNodsIndex].position);







    }

    private void Update()
    {
       
        animator.SetBool("run", false);
        PlayerDetection = Physics.CheckSphere(transform.position, radioView, PlayerCape);

        if (PlayerDetection == true)
        {


            //agent.isStopped = true;    
            transform.LookAt(GameManager.instance.player.transform.position);

            animator.SetBool("run", true);
            transform.position = Vector3.MoveTowards(transform.position, GameManager.instance.player.transform.position, speed * Time.deltaTime);


        }

        animator.SetBool("walk", true);
        var disToWaypoint = Vector3.Distance(Nods[CurrentNodsIndex].position, transform.position);

        if (disToWaypoint <= distToCheck)
        {

            NextPoint();
            print($"check{Nods[CurrentNodsIndex].name}.");




        }


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radioView);
    }
    void NextPoint() 
    { 
        CurrentNodsIndex = (CurrentNodsIndex + 1) % Nods.Length;
        agent.SetDestination(Nods[CurrentNodsIndex].position);
        animator.SetBool("walk", true);

    }
   


}







