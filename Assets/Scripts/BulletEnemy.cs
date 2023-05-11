using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] float lifetime;
    [SerializeField] float speed;
    public LayerMask playerMask;

    

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == playerMask)
        {
            if (c.GetComponent<Life_Player>() != null)
            {

                c.GetComponent<Life_Player>().Dano(10);
                Destroy(gameObject);
            }




        }
    }
}
