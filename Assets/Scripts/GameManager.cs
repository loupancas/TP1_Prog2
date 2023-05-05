using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //static entonces sera unico y se podra acceder desde cualquier lado
    public void Awake()
    {

        if (instance != null)  // si aparece otro e intenta coronarse se eliminar�
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this; // corona al primero que aparece
        }


    }
    public bool playerExist()
    {
        return player != null;
    }
    public player player;

    

}
