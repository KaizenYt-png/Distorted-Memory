using Interaction_System;
using UnityEngine;

public class TakeKeyInteractable : InteractableBase
{
    public override void OnInteract()
    {
        base.OnInteract();
        Destroy(gameObject);
    }
    
}
