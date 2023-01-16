using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Rotate rotate;
    public RectTransform rectTransform;

    public enum Rotate
    {
        _0,
        _90,
        _180,
        _270
    }
    
    
    
    public void OnBeginDrag(PointerEventData data)
    {
        
    }
    public void OnDrag(PointerEventData data)
    {
        rectTransform.anchoredPosition = data.position;
    }
    public void OnEndDrag(PointerEventData data)
    {
        
    }
}
