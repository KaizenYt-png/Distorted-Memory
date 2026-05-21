using Interaction_System;
using Player_Script;
using UnityEngine;

public class TakeKeyInteractable : InteractableBase
{
    [SerializeField] private string keyId;

    public override void OnInteract()
    {
        base.OnInteract();
        
        PlayerInventory playerInventory = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerInventory>();

        if (playerInventory == null)
        {
            Debug.LogWarning("PlayerInventory not found on Player");
            return;
        }

        if (string.IsNullOrWhiteSpace(keyId))
        {
            Debug.LogWarning("Key ID is missing on this key object.");
            return;
        }

        playerInventory.TakeKey(keyId);
        Destroy(gameObject);
    }
}
