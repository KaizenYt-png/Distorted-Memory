using Interaction_System;
using Player_Script;
using UnityEngine;

namespace Interactables
{
    public class UseDoorInteractable : InteractableBase
    {
        [SerializeField] private Door door;
        [SerializeField] private bool requiresKey;
        [SerializeField] private string requiredKeyId;

        private Transform _playerTransform;
        private PlayerInventory _playerInventory;

        private void Awake()
        {
            if (door == null)
                door = GetComponentInParent<Door>();
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            _playerTransform = player?.transform;
            _playerInventory = player?.GetComponent<PlayerInventory>();

            Debug.Log($"Awake -> door: {(door != null)}, playerTransform: {(_playerTransform != null)}");
        }

        public override void OnInteract()
        {
            base.OnInteract();

            Debug.Log("Door interact called");

            if (door == null)
            {
                Debug.LogWarning("Door reference is missing");
                return;
            }

            if (_playerTransform == null)
            {
                Debug.LogWarning("Player transform is missing");
                return;
            }
            
            if (requiresKey && _playerInventory == null)
            {
                Debug.LogWarning("PlayerInventory is missing on Player");
                return;
            }

            if (requiresKey && string.IsNullOrWhiteSpace(requiredKeyId))
            {
                Debug.LogWarning("Required Key ID is missing on this door.");
                return;
            }
            
            if (requiresKey && !door.IsOpen && !_playerInventory.HasKey(requiredKeyId))
            {
                TooltipMessage = $"Need : {requiredKeyId}";
                Debug.Log($"You need this key to open the door: {requiredKeyId}");
                return;
            }

            if (door.IsOpen)
            {
                TooltipMessage = "(E) Open";
                door.CloseDoor();
            }
            else
            {
                TooltipMessage = "(E) Close";
                door.OpenDoor(_playerTransform.position);
            }
        }
    }
}