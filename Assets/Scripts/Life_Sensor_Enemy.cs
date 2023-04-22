using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life_Sensor_Enemy : MonoBehaviour
{
    public float life;
    public int damage;
    public Image MyBar;
    public int Max;
  
    void Start()
    {

        life = Max;

    }
    private void OnTriggerEnter(Collider other)
    {
       Atack_Player Atack = other.GetComponent<Atack_Player>();
       
        
        
        if (Atack != null)
        {
            life -= Atack.damage;

            if (life <= 0)
            {
                life = 0;
                OnDie();


            }
          
            MyBar.fillAmount = life / Max;
        }
         
    }

    void OnDie()
    {

        Destroy(gameObject);


    }

    
}

    


