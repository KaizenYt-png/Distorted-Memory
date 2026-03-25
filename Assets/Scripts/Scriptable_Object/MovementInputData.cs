using UnityEngine;

namespace Scriptable_Object
{
    [CreateAssetMenu(fileName = "MovementInputData", menuName = "FirstPersonController/Data/MovementInputData", order = 1)]
    public class MovementInputData : ScriptableObject
    {
        static Vector2 _mInputVector;
        
        bool _mIsRunning;
        bool _mIsCrouching;

        bool _mCrouchClicked;
        bool _mJumpClicked;

        bool _mRunClicked;
        bool _mRunReleased;
        
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

        public bool IsRunning
        {
            get => _mIsRunning;
            set => _mIsRunning = value;
        }

        public bool IsCrouching
        {
            get => _mIsCrouching;
            set => _mIsCrouching = value;
        }

        public bool CrouchClicked
        {
            get => _mCrouchClicked;
            set => _mCrouchClicked = value;
        }

        public bool JumpClicked
        {
            get => _mJumpClicked;
            set => _mJumpClicked = value;
        }

        public bool RunClicked
        {
            get => _mRunClicked;
            set => _mRunClicked = value;
        }

        public bool RunReleased
        {
            get => _mRunReleased;
            set => _mRunReleased = value;
        }
        
        
        public void ResetInput()
        {
            _mInputVector = Vector2.zero;
                
            _mIsRunning = false;
            _mIsCrouching = false;

            _mCrouchClicked = false;
            _mJumpClicked = false;
            _mRunClicked = false;
            _mRunReleased =false;
        }
    }
}