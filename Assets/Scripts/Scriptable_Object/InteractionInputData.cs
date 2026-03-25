using UnityEngine;

namespace Scriptable_Object
{
    [CreateAssetMenu(fileName = "InteractionInputData", menuName = "InterecationSystem/InputData")]
    public class InteractionInputData : ScriptableObject
    {
    
        public bool InteractClick { get; set; }
        public bool InteractRelease { get; set; }

        public void ResetInput()
        {
            InteractClick = false;
            InteractRelease = false;
        }
    }
}
