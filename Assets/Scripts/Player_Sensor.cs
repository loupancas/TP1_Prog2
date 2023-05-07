using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player_Sensor : MonoBehaviour
{
    [SerializeField] UnityEvent EV_OnplayerEnter;
    [SerializeField] UnityEvent EV_OnplayerExit;
    [SerializeField] LayerMask playermask;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==playermask)
        {
            if (isPlayer(other))
            {
                print("main player");
                EV_OnplayerEnter.Invoke();
            }

        }
        
        
        
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playermask)
        {
            if (isPlayer(other))
            {
                print("player exit");
                EV_OnplayerExit.Invoke();
            }
        }

         


    }

    //private void OnTriggerStay(Collider other)
    //{
        //if (other.gameObject.layer == playermask)
        //{

            //if (isPlayer(other))
            //{
                //print("player stay");

            //}
        //}



    ///}





    bool isPlayer(Collider col)
    {
        player c = col.GetComponent<player>();

        if (c == GameManager.instance.GetPlayer())
        {
            return true;

        }

        return false;
    }


}
