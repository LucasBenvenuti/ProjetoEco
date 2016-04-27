using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    Vector3 StartPosition;
    public static Transform StartParent;
    public Transform TemporaryParent;

    void Start()
    {
        TemporaryParent = transform.parent.parent.parent;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        print("OnBeginDrag");
        itemBeingDragged = gameObject;
        StartPosition = transform.position;
        StartParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("Drag");
        transform.SetParent(TemporaryParent);
        transform.position = Input.mousePosition;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("OnEndDrag");
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (transform.parent != StartParent && transform.parent == TemporaryParent.transform)
        {
            transform.parent = StartParent;
            transform.position = StartPosition;

        }
        
    }
}
