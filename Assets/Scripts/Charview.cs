using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charview : MonoBehaviour
{
    Animator myAnim;

    private void Awake()
    {
        myAnim = GetComponentInChildren<Animator>(); //busca en los hijos
    }

    public void isRunning(bool running)
    {
        myAnim.SetBool("isRunning", running);
    }

    public void horizontal(float horiz)
    {
        myAnim.SetFloat("Horizontal", horiz);
    }
    public void vertical(float vert)
    {
        myAnim.SetFloat("Vertical", vert);
    }
}
