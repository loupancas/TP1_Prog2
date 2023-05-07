using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //static entonces sera unico y se podra acceder desde cualquier lado
    public void Awake()
    {

        if (instance != null)  // si aparece otro e intenta coronarse se eliminará
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this; // corona al primero que aparece
        }

        player = FindObjectOfType<player>();

    }
    public bool playerExist()
    {
        return player != null;
    }

    public player GetPlayer()
    {
        return player;
    }

    public player player;

    

}
