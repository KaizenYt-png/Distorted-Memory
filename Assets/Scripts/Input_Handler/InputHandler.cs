using Scriptable_Object;
using UnityEngine;

namespace Input_Handler
{
    public class InputHandler : MonoBehaviour
    {
    
        public InteractionInputData interactionInputData;
        public MovementInputData movementInputData;
        void Start()
        {
            interactionInputData.ResetInput();
            movementInputData.ResetInput();
        }

        // Update is called once per frame
        void Update()
        {
            GetInteractionInputData();
            GetMovementInputData();
        }
    
        void GetInteractionInputData()
        {
            interactionInputData.InteractClick = Input.GetKeyDown(KeyCode.E);
            interactionInputData.InteractRelease = Input.GetKeyUp(KeyCode.E);
        }

        void GetMovementInputData()
        {
            movementInputData.HorizontalInput = Input.GetAxisRaw("Horizontal");
            movementInputData.VerticalInput = Input.GetAxisRaw("Vertical");

            movementInputData.RunClicked = Input.GetKeyDown(KeyCode.LeftShift);
            movementInputData.RunReleased = Input.GetKeyUp(KeyCode.LeftShift);
        
            movementInputData.CrouchClicked = Input.GetKeyDown(KeyCode.LeftControl);
            movementInputData.CrouchReleased = Input.GetKeyUp(KeyCode.LeftControl);

            if(movementInputData.RunClicked)
                movementInputData.IsRunning = true;

            if(movementInputData.RunReleased)
                movementInputData.IsRunning = false;
        
            if(movementInputData.CrouchClicked)
                movementInputData.IsCrouching= true;

            if(movementInputData.CrouchReleased)
                movementInputData.IsCrouching = false;

        
            movementInputData.JumpClicked = Input.GetKeyDown(KeyCode.Space);
        
        }
    }
}
