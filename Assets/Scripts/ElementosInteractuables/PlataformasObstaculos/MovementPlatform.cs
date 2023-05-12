using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    public GameObject[] waypoints;
    public float platformSpeed = 2;
    private int waypointsIndex = 0;

        
    
    void Update()
    {
        movePlatform();
    }


    void movePlatform()
    {
        if(Vector3.Distance(transform.position,waypoints[waypointsIndex].transform.position)<0.1f)
        {
            waypointsIndex++;

            if(waypointsIndex>= waypoints.Length)
            {
                waypointsIndex = 0;
            }


        }

        transform.position = Vector3.MoveTowards(transform.position,waypoints[waypointsIndex].transform.position,platformSpeed*Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        Life_Player player = other.GetComponent<Life_Player>();
        if (player != null)
        {
            var controller = player.parent.GetComponent<CharacterController>();
            controller.transform.position = this.transform.position;
 
            Transform trans = player.parent;
            trans.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Life_Player player = other.GetComponent<Life_Player>();
        if (player != null)
        {
            Transform trans = player.parent;
            trans.SetParent(null);
        }
    }
}
