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
    [SerializeField] public LifeEnemy lifetaken;
   
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public BulletEnemy projectile;
 
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
        animator.SetBool("run", false);
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
        animator.SetBool("run", true);
        animator.SetBool("walk", false);
        agent.SetDestination(player.position);
        
    }

    private void AttackPlayer()
    {
        
        animator.SetBool("walk", false);
        animator.SetBool("run", false);

        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            
            
            BulletEnemy obj = GameObject.Instantiate(projectile);
            obj.Move(spawnPoint.position, spawnPoint.forward, 1f, 30f);

            ///End of attack code
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

           
          
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        lifetaken.healthPoint -= damage;

        if (lifetaken.healthPoint <= 0) Invoke(nameof(DestroyEnemy), 0.5f);

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
    void OnCollisionEnter(Collision col){
            if(col.gameObject.CompareTag("bullet")){
             TakeDamage(25);
            }
        }
}
