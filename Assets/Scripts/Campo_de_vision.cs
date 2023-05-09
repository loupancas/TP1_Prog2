using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campo_de_vision : MonoBehaviour
{
    public float radio; [Range(0,360)] // limitar el rango del enemigo
    public float angulo;

    public GameObject Referencia;

    public LayerMask PlayerMask;
    public LayerMask ObstruccionMask;

    public bool ver_Player;

    public void Start()
    {
        Referencia = GameObject.FindGameObjectWithTag("Player"); // Encontrar jugador
        StartCoroutine(Campo_de_vision_Rutina()); // Iniciar Rutina
    }

    private IEnumerator Campo_de_vision_Rutina()
    {
        float retraso = 0.2f;
        WaitForSeconds espera = new WaitForSeconds(retraso);

        while (true)
        {
            yield return espera;
            Campo_de_visionChequeo();
        }
    }

    private void Campo_de_visionChequeo()
    {
        Collider[] chequeo_rango = Physics.OverlapSphere(transform.position, radio, PlayerMask); //buscar los objetos en esa capa

        if (chequeo_rango.Length != 0) // Encuentra algo en esa capa
        {
            Transform objetivo = chequeo_rango[0].transform;
            Vector3 direccion_objetivo = (objetivo.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direccion_objetivo) < angulo / 2)
            {
                float distancia_objetivo = Vector3.Distance(transform.position, objetivo.position);

                if (Physics.Raycast(transform.position, direccion_objetivo, distancia_objetivo, ObstruccionMask))
                {
                    ver_Player = true;
                }
                else
                {
                    ver_Player = false;
                }
            }
            else ver_Player = false;
        }
        else if (ver_Player) // si pierde la vision del player
        {
            ver_Player = false;
        }
            
    }
}
