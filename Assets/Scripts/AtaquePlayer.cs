using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquePlayer : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    public float shotForce = 1500f;
    public float shotRate = 0.5f;
    private float shotRateTime = 0; // contador



    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > shotRateTime)
            {
                GameObject newBullet;
                newBullet = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce); // vector desde nuestro punto spwan
                shotRateTime = Time.time + shotRate;

                Destroy(newBullet, 4);

            }





        }






    }
}
