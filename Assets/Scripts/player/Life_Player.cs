using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Life_Player : MonoBehaviour
{
    public Image vidaSlider;
    public Transform parent;
    void Start()
    {
        //if (DataManager.instance == null)
        //{
            //SceneManager.LoadScene("Menu");
        //}
        //else
        //{

            vidaSlider.fillAmount = (float)DataManager.vida / DataManager.vidaMaxima;
        //}
    }
    public void Dano(int dmg)
    {

        DataManager.vida -= dmg;

        
        vidaSlider.fillAmount = (float)DataManager.vida / DataManager.vidaMaxima;
        Debug.Log((float)DataManager.vida / DataManager.vidaMaxima);
        Debug.Log("Tu vida es " + DataManager.vida);

        if (DataManager.vida < 25)
        {
            Debug.Log("Estas a punto de morir ");
            if (DataManager.vida <= 0)
            {

                Debug.Log("Perdiste");

                //Invoke("loadScene", 3f);
                Destroy(this.gameObject);
                DataManager.vida = DataManager.vidaMaxima;
                SceneManager.LoadScene("Fin");
            }
        }


    }
}
