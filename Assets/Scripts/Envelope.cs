using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envelope : MonoBehaviour
{
    public Action<string> clicked;

    public string letterName = "";

    void Start()
    {
        BoxCollider2D b = gameObject.GetComponent<BoxCollider2D>();
    }

    public void SetHighlight(bool isHighlight)
    {
        // TODO: Do highlight
    }
    
    //
    void OnMouseDown ()
    {
        clicked?.Invoke(letterName);
    }
}
