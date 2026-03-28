using Interaction_System;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractionData", menuName = "InterecationSystem/InteractionData")]
public class InteractionData : ScriptableObject
{
    public InteractableBase Interactable { get; set; }

    public void Interact()
    {
        Interactable.OnInteract();
        ResetData();
    }
    
    public bool IsSameInteractable(InteractableBase _newInteractable) => Interactable == _newInteractable;
    public bool IsEmpty => Interactable == null;
    
    public void ResetData() => Interactable = null;
    
}
