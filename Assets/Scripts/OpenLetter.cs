using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OpenLetter : MonoBehaviour
{
    public Transform paper;

    public bool isFlippable = true; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        FlipPaper();
    }

    void FlipPaper()
    {
        if (isFlippable){
            float nextRotation = paper.transform.eulerAngles.y == 0 ? 180 : 0;
            Debug.Log(nextRotation);
            paper.transform.DORotate(new Vector3(0, nextRotation, 0),0.5f);
        }
    }
}
