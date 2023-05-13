using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    public float rangodeAlerta;
    public float rangodeAttack;
    public float expDist = 1;
    public LayerMask playerMask;
    bool alerta;
    public Transform player;
    public float velocity;

    public float waveExplosionDist = 7f;

    //public ParticleSystem explosionparticle;
    [SerializeField] public LifeEnemy lifetaken;


    bool jumping = false;
    Rigidbody myRig;
    [SerializeField] float jumpForce;

    void Start()
    {
        myRig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(this.transform.position, player.transform.position);

        if (dist < rangodeAlerta)
        {
            if (dist < rangodeAttack)
            {
                if (!jumping)
                {
                    
                    jumping = true;
                    Vector3 dir = player.transform.position - transform.position;
                    myRig.AddForce(dir * jumpForce, ForceMode.VelocityChange);
                }

                if (dist < expDist)
                {
                    Collider[] cols =  Physics.OverlapSphere(this.transform.position, waveExplosionDist);

                    for (int i = 0; i < cols.Length; i++)
                    {
                        //pregunto si son explotables
                        //cajas
                        //objetos pequeños
                        //personaje

                        //McMovement player = cols[i].GetComponent<McMovement>();

                        //if (player != null)
                        //{
                            //player.TakeDamage(20);
                            
                        //}

                        Life_Player player= cols[i].GetComponent<Life_Player>();
                        if (player != null)
                        {
                            player.Dano(20);

                        }



                    }

                    //GameObject.Instantiate(explosionparticle,transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
            else
            {
                myRig.velocity = new Vector3(0,0,0);
                jumping = false;
                Vector3 posPlayer = new Vector3(player.position.x, transform.position.y, player.position.z);
                transform.LookAt(posPlayer);
                transform.position = Vector3.MoveTowards(transform.position, posPlayer, velocity * Time.deltaTime);
            }
        }
        else
        {
            jumping = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangodeAlerta);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangodeAttack);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, expDist);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, waveExplosionDist);

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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("bullet"))
        {
            TakeDamage(25);
        }
    }


}
