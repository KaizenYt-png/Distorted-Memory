using UnityEngine;

[CreateAssetMenu(fileName = "InteractionInputData", menuName = "InterecationSystem/InputData")]
public class InteractionInputData : ScriptableObject
{
    
    private bool m_interactClick;
    private bool m_interactRelease;

    public bool InteractClick
    {
        get => m_interactClick;
        set => m_interactClick = value;
    }

    public bool InteractRelease
    {
        get => m_interactRelease;
        set => m_interactRelease = value;
    }

    public void Reset()
    {
        m_interactClick = false;
        m_interactRelease = false;
    }
}
