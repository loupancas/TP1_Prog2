using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    void Start(){

       Hidemouse();
    }
   public void Showmouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void Hidemouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
