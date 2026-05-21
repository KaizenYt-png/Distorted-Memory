using System.Collections.Generic;
using UnityEngine;

namespace Player_Script
{
    public class PlayerInventory : MonoBehaviour
    {
        private readonly HashSet<string> _keys = new HashSet<string>();

        public void TakeKey(string keyId)
        {
            if (string.IsNullOrWhiteSpace(keyId))
            {
                Debug.LogWarning("Tried to take a key with no key ID.");
                return;
            }

            _keys.Add(keyId);
            Debug.Log($"Key taken: {keyId}");
        }

        public bool HasKey(string keyId)
        {
            return !string.IsNullOrWhiteSpace(keyId) && _keys.Contains(keyId);
        }
    }
}
