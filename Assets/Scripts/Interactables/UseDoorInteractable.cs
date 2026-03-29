using Interaction_System;
using UnityEngine;

namespace Interactables
{
    public class UseDoorInteractable : InteractableBase
    {
        [SerializeField] private Door door;
        private Transform _playerTransform;

        private void Awake()
        {
            if (door == null)
                door = GetComponentInParent<Door>();

            _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

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

            if (door.IsOpen)
                door.CloseDoor();
            else
                door.OpenDoor(_playerTransform.position);
        }
    }
}
