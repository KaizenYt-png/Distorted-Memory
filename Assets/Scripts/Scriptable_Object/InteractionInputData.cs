using UnityEngine;

[CreateAssetMenu(fileName = "InteractionInputData", menuName = "InterecationSystem/InputData")]
public class InteractionInputData : ScriptableObject
{
    
    private bool _mInteractClick;
    private bool _mInteractRelease;

    public bool InteractClick
    {
        get => _mInteractClick;
        set => _mInteractClick = value;
    }

    public bool InteractRelease
    {
        get => _mInteractRelease;
        set => _mInteractRelease = value;
    }

    public void ResetInput()
    {
        _mInteractClick = false;
        _mInteractRelease = false;
    }
}
