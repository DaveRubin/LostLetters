using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    public void UpdateTimer(float value)
    {
        // Debug.Log(value);
        image.fillAmount = value;
    }
}