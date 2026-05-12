using Interaction_System;
using Player_Script;
using UnityEngine;

public class TakeKeyInteractable : InteractableBase
{
    public override void OnInteract()
    {
        base.OnInteract();
        
        PlayerInventory playerInventory = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerInventory>();

        if (playerInventory == null)
        {
            Debug.LogWarning("PlayerInventory not found on Player");
            return;
        }

        playerInventory.TakeKey();
        Destroy(gameObject);
    }
}
