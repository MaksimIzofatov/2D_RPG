
    using System;
    using Level;
    using UnityEngine;

    public class CollisionHandlers : MonoBehaviour
    {
        public event Action<IInteractable> InteractableObjectIsNear;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.TryGetComponent(out IInteractable value))
            {
                InteractableObjectIsNear?.Invoke(value);
            }
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            if(other.gameObject.TryGetComponent(out IInteractable _))
            {
                InteractableObjectIsNear?.Invoke(null);
            }
        }
    }
