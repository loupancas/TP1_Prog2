using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    //public Shot shotDamage;
    public Transform spawnPoint;
    public Animator animator;


    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

   
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public GameObject newProyectil;
 
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
   
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        animator.SetBool("walk", true);
        if (!walkPointSet) SearchWalkPoint();
        

        if (walkPointSet)
            agent.SetDestination(walkPoint);
            
        


        Vector3 distanceToWalkPoint = transform.position - walkPoint;


        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
        
    }

    private void ChasePlayer()
    {
        //animator.SetBool("run", true);
        agent.SetDestination(player.position);
        
    }

    private void AttackPlayer()
    {
        
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            
            
            newProyectil = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            newProyectil.GetComponent<Rigidbody>().AddForce(transform.forward * 32f, ForceMode.Impulse);
            newProyectil.GetComponent<Rigidbody>().AddForce(transform.up * 8f, ForceMode.Impulse);
           /* Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);*/
            ///End of attack code
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

           
          
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        Destroy(newProyectil);
       
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);

    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
   /* void OnCollisionEnter(Collision col){
            if(col.gameObject.CompareTag("bullet")){
             TakeDamage(shotDamage.BulletDamage);
            }
        }*/
}
