using Scriptable_Object;
using UnityEngine;

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
        Debug.Log("E Clicked" + interactionInputData.InteractClick);
        interactionInputData.InteractRelease = Input.GetKeyUp(KeyCode.E);
        Debug.Log("E released" + interactionInputData.InteractClick);
    }

    void GetMovementInputData()
    {
        movementInputData.HorizontalInput = Input.GetAxisRaw("Horizontal");
        movementInputData.VerticalInput = Input.GetAxisRaw("Vertical");

        movementInputData.RunClicked = Input.GetKeyDown(KeyCode.LeftShift);
        movementInputData.RunReleased = Input.GetKeyUp(KeyCode.LeftShift);

        if(movementInputData.RunClicked)
            movementInputData.IsRunning = true;

        if(movementInputData.RunReleased)
            movementInputData.IsRunning = false;

        movementInputData.JumpClicked = Input.GetKeyDown(KeyCode.Space);
        movementInputData.CrouchClicked = Input.GetKeyDown(KeyCode.LeftControl);
    }
}
