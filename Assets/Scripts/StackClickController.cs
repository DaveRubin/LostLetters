using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace
{
    public class StackClickController : MonoBehaviour
    {
        public void SetInteractive(bool isInteractive)
        {
            Debug.Log($"Mailbox SetClickable {isInteractive}");
            this.isInteractive = isInteractive;
            // TODO: if highlight, stop the highlight
        }
        
        public bool isInteractive = false;
        public event Action mouseEnter;
        public event Action mouseLeave;
        public event Action mouseClick;
        public void OnMouseEnter()
        {
            if (!isInteractive)
            {
                return;
            }
            Debug.Log("StackOnMouseEnter");
            mouseEnter.Invoke();            
        }
        
        public void OnMouseExit()
        {
            if (!isInteractive)
            {
                return;
            }
            Debug.Log("StackOnMouseLeave");
            mouseLeave.Invoke();            
        }
        
        public void OnMouseDown()
        {
            if (!isInteractive)
            {
                return;
            }
            Debug.Log("StackMouseDown");
            mouseClick.Invoke();     
        }
    }
}