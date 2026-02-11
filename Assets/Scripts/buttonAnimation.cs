using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;

public class buttonAnimation : MonoBehaviour ,
 IPointerEnterHandler, IPointerExitHandler ,
IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float buttonScale = 1.2f;
    [SerializeField] private float buttonDownScale = 0.8f;

    private bool isPressed;
    private Vector3 originalScale;


    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       isPressed = true;
       transform.localScale = originalScale * buttonScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       isPressed = false;
         transform.localScale = originalScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      
        transform.localScale = originalScale * buttonDownScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPressed)
        {
            transform.localScale = originalScale * buttonScale;
        }
        else
        {
            transform.localScale = originalScale;
        }
    }
       
}
