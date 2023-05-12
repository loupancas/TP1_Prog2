using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] float lifetime;
    [SerializeField] float speed;
    public LayerMask playerMask;
    public bool isEnemyBullet;
    public int damage;

    private void Update()
    {
        bulletMove();
    }

    void bulletMove()
    {

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) Destroy(gameObject);


    }



    private void OnTriggerEnter(Collider collision) // n tener RigidBody
    {

        var B = collision.gameObject.GetComponent<BulletPlayer>();

        if (B != null) 
        {
            return;
        }

        if (!isEnemyBullet)
        {
            LifeEnemy HacerDano = collision.gameObject.GetComponent<LifeEnemy>(); // esta trayendo el codigo 


            if (HacerDano)
            {
                Debug.Log("el daño realizado es" + damage);
                HacerDano.golpe(damage); // llama a la funcion golpe del otro script

            }

            if (collision.gameObject.tag != "Player") // se destruirá con todo lo que no sea el PJ o sea los enemigos
            {

                Destroy(gameObject);

            }
        }

    }
}
