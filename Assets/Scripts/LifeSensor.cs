using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSensor : MonoBehaviour
{
    
    public float life;
    public int damage;
    public Image MyBar;
    public int Max;
   

    private void Start()
    {
        life = Max;
    }

    private void OnTriggerEnter(Collider other)
    {
       //Projectile MyProjectile = other.gameObject.GetComponent<Projectile>();
        MoveEnemy MyEnemyDamage = other.gameObject.GetComponent<MoveEnemy>();
        Item_Life Myitem_Life = other.gameObject.GetComponent<Item_Life>();


        //if (MyProjectile)
        //{
        //    life -= MyProjectile.damage;

        //    if (life <= 0)
        //    {
        //        life = 0;
        //        OnDie();


        //    }

        //}
        if (MyEnemyDamage != null)
        {
            life -= MyEnemyDamage.damage;

            if (life <= 0)
            {
                life = 0;
                OnDie();


            }

            MyBar.fillAmount = life / Max;

        }


        if (Myitem_Life)
        {
            life += Myitem_Life.ItemLife;

            if (life >= Max)
            {
                life = Max;
            }

            Destroy(Myitem_Life.gameObject);

        }
       


        MyBar.fillAmount = life / Max;
    
    }
    void OnDie() 
    {

        Destroy(gameObject);
        
    
    }


    

}  
