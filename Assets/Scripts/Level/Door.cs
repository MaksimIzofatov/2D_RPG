
    using UnityEngine;

    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _doorDestroy;
        public void Interact()
        {
            _doorDestroy.SetActive(false);
        }
    }
