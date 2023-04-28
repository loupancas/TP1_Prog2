using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobertMovement : MonoBehaviour
{
  public int rutine;
    public float timer;
    public Animator ani;
    public Quaternion ang;
    public float grav; 
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame

    public void BehaviourEnemy(){


        timer+= 1*Time.deltaTime;
        if(timer>=4){
            rutine = Random.Range(0,2);
            timer = 0;
        }
        
        switch(rutine){
            case 0:
                //nothing happens
                ani.SetBool("Idle",true);
                ani.SetBool("walk",false);
             break;
            case 1:
                grav = Random.Range(0,360);
                ang = Quaternion.Euler(0,grav,0);
                rutine++;
                break;
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, ang, 0.5f);
                transform.Translate(Vector3.forward*3*Time.deltaTime);
                ani.SetBool("walk",true);
                break;


        }
    }
        void Update()
    {
        BehaviourEnemy();
    }

}
