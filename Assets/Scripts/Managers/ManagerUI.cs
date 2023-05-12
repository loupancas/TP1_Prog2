using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    public Image barLife;

    public void UpdateLife(float life, float maxlife)
    {
        barLife.fillAmount = life / maxlife;
    }
}
