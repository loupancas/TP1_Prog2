using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeEnemy : MonoBehaviour
{
    
    
    
    [SerializeField] private Image healthbarsprite;
    private float healthPoint;
    [SerializeField] private float maxHealthPoint;
    public bool puedeEliminarse;
    public float danoCritico;// se personaliza el dano critico según la vida total del enemigo
    [SerializeField] private Color cambiarColor = Color.red;
    [SerializeField] private Color colorInicial = Color.white;
    public Renderer rend;
    public bool recibeDano;
    float timer = 0;
    [SerializeField] float time_duration = 1f;






    protected virtual void Start()
    {
        healthPoint = maxHealthPoint;



    }

    private void Update()
    {
        

        updateHealthbar(maxHealthPoint, healthPoint);
       

        if (recibeDano)
        {
            if (timer < time_duration)
            {
                timer = timer + 1 * Time.deltaTime;
            }
            else
            {
                timer = 0;
                recibeDano = false;
                rend.material.color = colorInicial;
            }
        }



    }




    protected virtual void Death()
    {
        Destroy(gameObject);
    }

    public void updateHealthbar(float maxhealth, float currenthealth)
    {
        healthbarsprite.fillAmount = currenthealth / maxhealth;

    }


    public void golpe(int damageRecibido) // en cada golpe se irá perdiendo vida hasta que finalmente el enemigo se destruye , esto es una funcion publica
    {


        healthPoint = healthPoint - damageRecibido;



        if (healthPoint <= 0)
        {

            Debug.Log("Haz eliminado al enemigo");
            Death();
        }
        else
        {
            timer = 0;
            recibeDano = true;
            rend.material.color = cambiarColor;
        }




    }
}
