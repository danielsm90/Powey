using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class slot : MonoBehaviour, IDropHandler 
{
    public bool empty = true;
    

    public void OnDrop(PointerEventData eventData)
    {
        if (empty)
        {
            //Condición para poder soltar
            if (this.GetComponent<Image>().sprite.name != "Ciudad_Final-02" && this.GetComponent<Image>().sprite.name != "ventanas-06")
            {
                //Destroy(eventData.pointerDrag);

                //Set color (Solo para ejemplo)
                this.GetComponent<Image>().sprite = Resources.Load<Sprite>("img/Nivel 1/Ciudad_Final-05");

                //Set slot as occupied
                Aleatorio.aleatorio.cantLedCamb++;
                Aleatorio.aleatorio.cantDinero++;
                Aleatorio.aleatorio.sonidoDinero.Play();
                empty = false;
            }
            else
            {
                //sonido perdio bombilla y no cambia ventana
                Aleatorio.aleatorio.corto.Play();
            }
            //resta bombilla led
            Aleatorio.aleatorio.cantLed--;
        }
    }    

    
}