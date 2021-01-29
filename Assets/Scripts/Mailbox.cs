using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Mailbox : MonoBehaviour
    {
        public bool isInteractive = false;
        public Transform targetEnvelopFront;
        public Transform targetEnvelopInside;
        public event Action mouseClicked;
        
        public void SetInteractive(bool isInteractive)
        {
            Debug.Log($"Mailbox SetClickable {isInteractive}");
            this.isInteractive = isInteractive;
            // TODO: if highlight, stop the highlight
        }

        public void OnMouseEnter()
        {
            Debug.Log("Mailbox OnMouseEnter");
            if (!isInteractive)
            {
                return;
            }
        }

        public void OnMouseExit()
        {
            Debug.Log("Mailbox OnMouseExit");
            if (!isInteractive)
            {
                return;
            }
        }

        public void OnMouseDown()
        {
            Debug.Log($"Mailbox OnMouseDown {isInteractive}");
            if (isInteractive)
            {
                mouseClicked?.Invoke();
            }
        }
    }
}