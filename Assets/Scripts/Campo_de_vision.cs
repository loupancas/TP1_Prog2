using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campo_de_vision : MonoBehaviour
{
    public float radio; [Range(0,360)] // limitar el rango del enemigo
    public float angulo;


    [SerializeField] public LifeEnemy lifetaken;

    public GameObject Referencia;

    public LayerMask PlayerMask;
    public LayerMask ObstruccionMask;

    public bool ver_Player;

    Renderer render;

    public void Start()
    {
        render = GetComponent<Renderer>();
        render.material.color = Color.green;
        Referencia = GameObject.FindGameObjectWithTag("Player"); // Encontrar jugador
        //StartCoroutine(Campo_de_vision_Rutina()); // Iniciar Rutina
    }

    //private IEnumerator Campo_de_vision_Rutina()
    //{
    //    //float retraso = 0.2f;
    //    //WaitForSeconds espera = new WaitForSeconds(retraso);

    //    //while (true)
    //    //{
    //    //    yield return espera;
    //    //    Campo_de_visionChequeo();
    //    //}
    //}
    float timer;
    public float cddamage;
    private void Update()
    {
        if (timer < cddamage)
        {
            timer = timer + 1 * Time.deltaTime;
        }
        else
        {
            timer = 0;
            Life_Player player;
            if (Campo_de_visionChequeo(out player))
            {
                player.Dano(5);
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
                    return true;
                }
                else
                {
                    render.material.color = Color.green;
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
        lifePlayer = null;
        return false;  
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radio);

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
