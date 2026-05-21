using UnityEngine;
using UnityEngine.Events;

namespace Object_Script
{
    public class DeathZone : MonoBehaviour
    {
        [SerializeField] private Transform respawnPoint;
        

        private void OnTriggerEnter(Collider other)
        {
            
            Transform player = other.transform.root;

            if (!player.CompareTag("Player"))
            {
                return;
            }

            if (respawnPoint == null)
            {
                Debug.LogWarning("Respawn point is not assigned.");
                return;
            }

            CharacterController characterController = player.GetComponent<CharacterController>();
            Rigidbody rigidbody = player.GetComponent<Rigidbody>();

            if (characterController != null)
            {
                characterController.enabled = false;
            }

            if (rigidbody != null)
            {
                rigidbody.linearVelocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }
            
            player.position = respawnPoint.position;
            

            if (characterController != null)
            {
                characterController.enabled = true;
            }

            Debug.Log($"Player root respawned to {respawnPoint.position}");
        }
    }
}