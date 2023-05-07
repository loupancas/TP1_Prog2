using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    List<int> llaves = new List<int>();
    public McMovement movement;//referencia al movimiento 
    public void obtieneLlave(int clave)
    {

        llaves.Add(clave);

    }

    public bool tieneLlave(int clave) // pregunta si tiene llave
    {
        if (llaves.Contains(clave))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
