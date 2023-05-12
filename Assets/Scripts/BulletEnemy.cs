using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    float lifetime;
    float speed;
    Vector3 dir;
    public LayerMask playerMask;

    float timer = 0;

    public void Move(Vector3 _pos, Vector3 _dir, float _lifetime, float _speed)
    {
        transform.position = _pos;
        transform.forward = _dir;
        lifetime = _lifetime;
        speed = _speed;
    }

    private void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;

        if (timer < lifetime)
        {
            timer = timer + 1 * Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        var obj = c.GetComponent<Life_Player>();
        if (obj != null)
        {
            obj.Dano(10);
            Destroy(gameObject);
        }
    }
}
