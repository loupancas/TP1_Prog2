using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_movement : MonoBehaviour
{
    public GameObject[] waypoints;
    public float platformSpeed=2;
    int waypointIndex = 0;

    void Update()
    {
        Mov_Platform();
    }

    void Mov_Platform()
    {

        if(Vector3.Distance(transform.position,waypoints[waypointIndex].transform.position)<0.1f)
        {

            waypointIndex++;
            if(waypointIndex>=waypoints.Length)
            {
                waypointIndex = 0;
            }


        }

        transform.position = Vector3.MoveTowards(transform.position,waypoints[waypointIndex].transform.position,platformSpeed*Time.deltaTime);



    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }




}
