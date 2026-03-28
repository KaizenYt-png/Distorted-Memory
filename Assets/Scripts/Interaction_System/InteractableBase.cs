using UnityEngine;

namespace Interaction_System
{
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        public float holdDuration;
        
        public bool holdInteract;
        public bool multipleUse;
        public bool isInteractable;
        
        [SerializeField] private string tooltipMessage = "Interact";

        public float HoldDuration => holdDuration;
        
        public bool HoldInteract => holdInteract;
        public bool MultipleUse => multipleUse;
        public bool IsInteractable => isInteractable;
        
        public string TooltipMessage => tooltipMessage;

        public virtual void OnInteract()
        {
            Debug.Log("Interacted" + gameObject.name);
        }
    }
}
