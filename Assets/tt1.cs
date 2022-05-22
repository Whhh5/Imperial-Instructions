using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tt1 : MonoBehaviour, IPointerEnterHandler,IPointerDownHandler,IPointerExitHandler,IPointerUpHandler,IPointerClickHandler
{
    const int a = 0;
    const int b = 1;
    readonly int c = 0;
    readonly GameObject d;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"OnPointClick");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"OnPointUp");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log($"OnPointExit");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"OnPointDown");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"OnPointEnter");
    }

    private void Update()
    {
        if (Input.GetKeyDown(key:KeyCode.Alpha1))
        {
            
        }
    }
}
