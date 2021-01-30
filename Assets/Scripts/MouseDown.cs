using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDown : MonoBehaviour
{
    // Start is called before the first frame update
    public Action onClicked;
    private void OnMouseDown()
    {
        onClicked?.Invoke();
    }
}
