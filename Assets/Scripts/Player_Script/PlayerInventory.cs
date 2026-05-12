using UnityEngine;

namespace Player_Script
{
    public class PlayerInventory : MonoBehaviour
    {
        public bool HasKey { get; private set; }

        public void TakeKey()
        {
            HasKey = true;
            Debug.Log("Key taken");
        }
    }
}
