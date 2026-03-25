using UnityEngine;

namespace Scriptable_Object
{
    [CreateAssetMenu(fileName = "MovementInputData", menuName = "FirstPersonController/Data/MovementInputData", order = 1)]
    public class MovementInputData : ScriptableObject
    {
        static Vector2 _mInputVector;

        public Vector2 InputVector => _mInputVector;
        public bool HasInput => _mInputVector != Vector2.zero;
        public float HorizontalInput
        {
            get => _mInputVector.x;
            set => _mInputVector.x = value;
        }

        public float VerticalInput
        {
            get => _mInputVector.y;
            set => _mInputVector.y = value;
        }

        public bool IsRunning { get; set; }

        public bool IsCrouching { get; set; }

        public bool CrouchClicked { get; set; }

        public bool CrouchReleased { get; set; }

        public bool JumpClicked { get; set; }

        public bool RunClicked { get; set; }

        public bool RunReleased { get; set; }


        public void ResetInput()
        {
            _mInputVector = Vector2.zero;
                
            IsRunning = false;
            IsCrouching = false;

            CrouchClicked = false;
            JumpClicked = false;
            RunClicked = false;
            RunReleased =false;
        }
    }
}