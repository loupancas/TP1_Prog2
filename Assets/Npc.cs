using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Npc : MonoBehaviour
{
    public GameObject simbolo;
    public McMovement jugador;
    public GameObject panelNPC;
    public TextMeshProUGUI textNPC;
    public GameObject mision; 
    public TextMeshProUGUI misionTEXT;
    public bool jugadorCerca;
    public bool aceptarMision;
    public GameObject[] objetivos; 
    public int numObj;
    public GameObject botonMision;

    // Start is called before the first frame update
    void Start()
    {
        numObj = objetivos.Length;
        textNPC.text = "Hey i need your help.";
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<McMovement>();
        simbolo.SetActive(true); 
        panelNPC.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)&& aceptarMision==false){
            Vector3 posJugador = new Vector3(transform.position.x, jugador.gameObject.transform.position.y, transform.position.z);
            jugador.gameObject.transform.LookAt(posJugador);
        }
    }

    private void OnTriggerEnter(Collider other){

        if(other.tag == "Player"){

            jugadorCerca = true;

            if(aceptarMision == false){
                 panelNPC.SetActive(true);
            }
            else{
                 mision.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.tag =="Player"){
            jugadorCerca = false;
            panelNPC.SetActive(false);
            mision.SetActive(false);
        }
    }

    public void Aceptar(){
         mision.SetActive(true);
        aceptarMision = true;
        jugadorCerca = false;
        simbolo.SetActive(false);
        panelNPC.SetActive(false);
       
    }
}
