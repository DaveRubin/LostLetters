using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Mailbox : MonoBehaviour
    {
        public ECharacters character = ECharacters.Daniel;
        public bool isInteractive = false;
        public event Action mouseClicked;
        private Image sprite;
        private static Color off = new Color(1, 1, 1, 0);
        private static Color on = new Color(1, 1, 1, 0.5f);

        private void Start()
        {
            sprite = GetComponent<Image>();
            sprite.DOColor(off, 0);   
        }

        public void SetInteractive(bool isInteractive)
        {
            Debug.Log($"Mailbox SetClickable {isInteractive}");
            this.isInteractive = isInteractive;
            if (!isInteractive)
            {
                sprite.DOColor(off, 0);   
            }
        }

        public void OnMouseEnter()
        {
            Debug.Log("Mailbox OnMouseEnter");
            if (!isInteractive)
            {
                return;
            }
            sprite.DOColor(on, 0);
        }

        public void OnMouseExit()
        {
            Debug.Log("Mailbox OnMouseExit");
            if (!isInteractive)
            {
                return;
            }
            sprite.DOColor(off, 0);
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