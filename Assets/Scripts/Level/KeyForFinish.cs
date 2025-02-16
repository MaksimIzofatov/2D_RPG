using Level;
using UnityEngine;


    public class KeyForFinish : MonoBehaviour, IInteractable
    {
        public bool IsActivate { get; private set; }
        public void Interact()
        {
            IsActivate = true;
            this.gameObject.SetActive(false);
        }
    }

