using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atack_Player : MonoBehaviour
{
    
    public Animator animator;
    public int damage;
    public Transform AtackPoint;
    public float AtackRange = 0.5f;
    public LayerMask Enemys;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Event_Atack() 
    {

        
        Collider[] hitenemies = Physics.OverlapCapsule(AtackPoint.position, AtackPoint.position,  AtackRange, Enemys);

        foreach (Collider enemy in hitenemies)
        {
            MoveEnemy enem = enemy.GetComponent<MoveEnemy>();
            if (enem != null)
            {
                Life_Sensor_Enemy life = enem.GetComponent<Life_Sensor_Enemy>();

                life.life = damage;
            }



        }


    }

    void Update()
    {
      Event_Atack();
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Atack");
            

        }
        



    }
    private void OnDrawGizmosSelected()
    {
        if(AtackPoint == null) 
        { return; }

        Gizmos.DrawWireSphere(AtackPoint.position, AtackRange);
    }


}

    