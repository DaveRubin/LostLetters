using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Envelope : MonoBehaviour
{
    public Action<string> clicked;

    public string letterName = "";

    // Start is called before the first frame update
    void Start()
    {
        // this.GetComponent(BoxCollider2D)
        BoxCollider2D b = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //
    void OnMouseDown ()
    {
        clicked?.Invoke(letterName);
    }
}
