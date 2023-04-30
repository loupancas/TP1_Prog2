using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemy : MonoBehaviour
{

    public int damage;
    public int rutina;
    public float speed;
    public float cron;
    public float grado;
    GameObject target;
    private NavMeshAgent agent;
    public Quaternion angle;
    public Animator animator;
    public float radioView;
    public float distAtack;
    public bool atack;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
        //target = GameManager.GetPlayer(); //crean un manager singleton para hacer esto
    }

    private void Update()
    {
        Move_Enemy();
    }
    public void Move_Enemy()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > radioView)
        {
            agent.enabled = false;
            animator.SetBool("run", false);
            cron += 1 * Time.deltaTime;
            

            if (cron >= 4)
            {
                rutina = Random.Range(0, 3);

                switch (rutina)
                {
                    case 0:
                        agent.enabled = false;
                        animator.SetBool("idel", false);
                        break;
                    
                    case 1:
                        grado = Random.Range(0, 360);
                        angle = Quaternion.Euler(0, grado, 0);
                       
                        rutina++;
                        break;
                    
                    case 2:
                        agent.enabled = true;
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                        transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        animator.SetBool("walk", true);
                        break;


                }

            }
            else
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);

                agent.enabled = true;
                agent.SetDestination(target.transform.position);

                if (Vector3.Distance(transform.position, target.transform.position) > distAtack && !atack)
                {
                    animator.SetBool("walk", false);
                    animator.SetBool("run", true);

                }
                else
                {
                    if (!atack)
                    {
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 1f);

                        animator.SetBool("walk", false);
                        animator.SetBool("run", false);
                    }


                }
            }

            if (atack) 
            { 
              agent.enabled = false;

            }

        }


    }


}







