using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Image image;
    private Transform dial;
    void Start()
    {
        dial = transform.Find("Dial");
        image = transform.Find("Fill").GetComponent<Image>();
    }

    public void UpdateTimer(float value)
    {
        // Debug.Log(value);
        float fillAmount = value;
        image.fillAmount = fillAmount;
        dial.localEulerAngles = new Vector3(0,0, fillAmount * -360);
    }
}