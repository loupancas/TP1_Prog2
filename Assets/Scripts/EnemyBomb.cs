using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    public float rangodeAlerta;
    public LayerMask playerMask;
    bool alerta;
    public Transform player;
    public float velocity;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       alerta= Physics.CheckSphere(transform.position, rangodeAlerta, playerMask); // esta alerta el enemy
        if(alerta==true)
        {
            
            Vector3 posPlayer=new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.LookAt(posPlayer);
            transform.position = Vector3.MoveTowards(transform.position, posPlayer,velocity*Time.deltaTime);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,rangodeAlerta);
       
    }


}
