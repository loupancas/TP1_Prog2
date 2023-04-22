using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    //public AudioSource clip;

    public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void QuitGame()
    {
        Application.Quit(); // termina el juego
    }

    public void SonidoBoton()
    {
        //clip.Play();
    }

}
