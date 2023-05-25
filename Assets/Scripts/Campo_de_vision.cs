using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campo_de_vision : MonoBehaviour
{
    public float radio;[Range(0, 360)] // limitar el rango del enemigo
    public float angulo;

    public GameObject Referencia;

    public LayerMask PlayerMask;
    public LayerMask ObstruccionMask;
    public new Light light;

    public bool ver_Player;

    Renderer render;

    public void Start()
    {
        render = GetComponent<Renderer>();
        render.material.color = Color.green;
        Referencia = GameObject.FindGameObjectWithTag("Player"); // Encontrar jugador

    }


    float timer;
    public float cddamage;
    private void Update()
    {
        if (timer < cddamage)
        {
            timer = timer + 0.5f * Time.deltaTime;
        }
        else
        {
            timer = 0;
            Life_Player player;
            if (Campo_de_visionChequeo(out player))
            {
                player.Dano(2);
            }
        }
    }

    private bool Campo_de_visionChequeo(out Life_Player lifePlayer)
    {
        Collider[] chequeo_rango = Physics.OverlapSphere(transform.position, radio, PlayerMask); //buscar los objetos en esa capa

        if (chequeo_rango.Length != 0) // Encuentra algo en esa capa
        {
            bool found = false;
            Life_Player life = null;
            lifePlayer = null;
            for (int i = 0; i < chequeo_rango.Length; i++)
            {
                Life_Player obj = chequeo_rango[i].transform.gameObject.GetComponent<Life_Player>();
                if (obj != null)
                {
                    lifePlayer = obj;
                    found = true;
                    life = obj;
                    break;
                }
            }
            if (!found) return false;

            Vector3 direccion_objetivo = (life.gameObject.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direccion_objetivo) < angulo / 2)
            {
                float distancia_objetivo = Vector3.Distance(transform.position, life.gameObject.transform.position);

                if (distancia_objetivo < radio)
                {
                    render.material.color = Color.red;
                    light.color = Color.red;
                    return true;

                }
                else
                {
                    render.material.color = Color.green;
                    light.color = Color.green;

                    return false;
                }
            }
            else
            {

                render.material.color = Color.green;
                return false;
            }
        }
        render.material.color = Color.green;
        light.color = Color.green;
        lifePlayer = null;
        return false;
    }

}
