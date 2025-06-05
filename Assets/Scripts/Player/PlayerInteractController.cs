using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInteractController : MonoBehaviour
    {
        public static Action<int> OnPlayerInteract;
        
        [SerializeField] InputActionReference inputActionReference;
        [SerializeField] GameObject interactPrompt;
        bool _canInteract = false;
        int _otherID = -1;
        
        const string InteractableTag = "Interactable";
        void Awake()
        {
            if (null != inputActionReference)
                inputActionReference.action.performed += OnInteractAction;
        }

        void OnDestroy()
        {
            if (null != inputActionReference)
                inputActionReference.action.performed -= OnInteractAction;
        }

        void OnInteractAction(InputAction.CallbackContext obj)
        {
            if (!_canInteract || _otherID == -1)
                return;

            OnPlayerInteract?.Invoke(_otherID);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(InteractableTag))
                return;

            _otherID = other.gameObject.GetInstanceID();
            _canInteract = true;
            interactPrompt.SetActive(true);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (!_canInteract)
                return;
            
            _otherID = -1;
            _canInteract = false;
            interactPrompt.SetActive(false);
        }
    }
}