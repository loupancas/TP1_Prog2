using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int clave;
    public bool entro; //public para verificar si entra o no en el inspector
    
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
        { // presiono tecla Q y entro 

            GameManager.instance.player.obtieneLlave(clave); // se le pasa la clave al player
            Destroy(this.gameObject);

        }




    }
}
