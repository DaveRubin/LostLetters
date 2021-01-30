using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Envelope : MonoBehaviour
{
    public Action<string> clicked;

    public string letterName = "";

    void Start()
    {
        int selectedStyle = Convert.ToInt32(Random.Range(1, 4));
        transform.Find("1").gameObject.SetActive(selectedStyle==1);
        transform.Find("2").gameObject.SetActive(selectedStyle==2);
        transform.Find("3").gameObject.SetActive(selectedStyle==3);
        transform.Find("4").gameObject.SetActive(selectedStyle==4);
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
