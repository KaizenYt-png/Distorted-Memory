using UnityEngine;

namespace Interaction_System
{
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        public float holdDuration;
        
        public bool holdInteract;
        public bool multipleUse;
        public bool isInteractable;

        public float HoldDuration => holdDuration;
        
        public bool HoldInteract => holdInteract;
        public bool MultipleUse => multipleUse;
        public bool IsInteractable => isInteractable;

        public virtual void OnInteract()
        {
            Debug.Log("Interacted" + gameObject.name);
        }
    }
}
