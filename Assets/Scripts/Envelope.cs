using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Envelope : MonoBehaviour
{
    public Action<string> clicked;
    public string letterName = "";
    private SpriteRenderer selectedSprite;
    private Color highlightColor = new Color(1, 0.5f, 0.5f);
    void Start()
    {
        int selectedStyle = Convert.ToInt32(Random.Range(1, 4));
        transform.Find("1").gameObject.SetActive(selectedStyle==1);
        transform.Find("2").gameObject.SetActive(selectedStyle==2);
        transform.Find("3").gameObject.SetActive(selectedStyle==3);
        transform.Find("4").gameObject.SetActive(selectedStyle==4);

        selectedSprite = transform.Find(selectedStyle.ToString()).GetComponent<SpriteRenderer>();
    }

    public void SetHighlight(bool isHighlight)
    {
        // TODO: Do highlight
        Debug.Log(isHighlight);
        selectedSprite.DOColor(isHighlight?highlightColor:Color.white , 0.5f);
    }
    
    //
    void OnMouseDown ()
    {
        Debug.Log(letterName);
        clicked?.Invoke(letterName);
    }
}
