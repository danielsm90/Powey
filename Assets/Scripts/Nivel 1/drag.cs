using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

	private Transform originalParent;
    public bool dragging = false;
    public Vector3 initialPosition;

	public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        //Guardamos la posición inicial
        initialPosition = this.transform.position;
                      
            //Para evitar que se oculte el objeto detras de otros
            originalParent = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position;
            
            //Para evitar que se bloqueen los eventos (Este era el error :/ :( )
            //Se necesita agregar un componente en los objetos que se arrastran (CanvasGroup)
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
    {
            this.transform.position = eventData.position;
	}

    //Retornamos el objeto a la posición inicial
    public void returnToInitialPosition()
    {
        this.transform.position = initialPosition;
    }

	public void OnEndDrag(PointerEventData eventData){
        this.transform.SetParent (originalParent);
        this.transform.position = initialPosition;
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
        
	}



}
