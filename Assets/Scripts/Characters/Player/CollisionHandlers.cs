
    using System;
    using Level;
    using UnityEngine;

    public class CollisionHandlers : MonoBehaviour
    {
        public event Action<IInteractable> InteractableObjectIsNear;
        private void OnTriggerEnter2D (Collider2D other)
        {
            if(other.gameObject.TryGetComponent(out IInteractable value))
            {
                InteractableObjectIsNear?.Invoke(value);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.TryGetComponent(out IInteractable _))
            {
                InteractableObjectIsNear?.Invoke(null);
            }
        }
    }
