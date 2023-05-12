using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject abierto;
    [SerializeField] GameObject cerrado;
    bool entro; //public para verificar si entra o no en el inspector

    public bool estaAbierto = false; // empieza puerta cerrada

    public int claveaIngresar = 0;

    private void Start()
    {
        if (estaAbierto)// si abri la puerta sucederá
        {
            abierto.SetActive(true);
            cerrado.SetActive(false);
        }
        else// si no abri abri la puerta sucederá
        {
            abierto.SetActive(false);
            cerrado.SetActive(true);
        }

    }


    #region DETECCION
    private void OnTriggerEnter(Collider collision) // para entrar al portal
    {
        player player = collision.GetComponent<player>(); // obtiene el player si este colisiono

        if (player != null) // si colisiono
        {

            entro = true;
        }
    }

    private void OnTriggerExit(Collider collision) // para salir del portal
    {
        player player = collision.GetComponent<player>(); // obtiene el player si este colisiono

        if (player != null) // no colisiono
        {
            entro = false;

        }
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && entro) //tecla Q no olvidar poner el instrucciones
        { // presiono tecla Q y entrar
            if (GameManager.instance.player.tieneLlave(claveaIngresar))
            {

                estaAbierto = !estaAbierto;

                if (estaAbierto)// si abri la puerta sucederá
                {
                    abierto.SetActive(true);
                    cerrado.SetActive(false);
                }
                else// si no abri abri la puerta sucederá
                {
                    abierto.SetActive(false);
                    cerrado.SetActive(true);
                }

            }


        }

    }
}
