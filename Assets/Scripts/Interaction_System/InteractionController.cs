using System;
using Interaction_System;
using Scriptable_Object;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [Space, Header("Data")]
    [SerializeField] private InteractionInputData interactionInputData;
    [SerializeField] private InteractionData interactionData;
    
    [Space, Header("UI")]
    [SerializeField] private InteractionUIPanel uiIPanel;
    
    [Space, Header("Ray Setting")]
    [SerializeField] private float rayDistance;
    [SerializeField] private float raySphereRadius;
    [SerializeField] private LayerMask interactableLayer;
    
    private Camera _mcamera;
    
    private bool _minteracting;
    private float _mholdTimer;

    private void Awake()
    {
        _mcamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        CheckForInteractable();
        CheckForInteractableInput();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void CheckForInteractableInput()
    {
        Ray _ray = new Ray(_mcamera.transform.position, _mcamera.transform.forward);
        RaycastHit _hitInfo;
        
        bool _hitSomething = Physics.SphereCast(_ray, raySphereRadius * rayDistance, out _hitInfo, raySphereRadius, interactableLayer);

        if (_hitSomething)
        {
            InteractableBase _interactable = _hitInfo.collider.GetComponent<InteractableBase>();
            if (_interactable != null)
            {
                if (interactionData.IsEmpty)
                {
                    interactionData.Interactable = _interactable;
                    uiIPanel.SetTooltip(_interactable.TooltipMessage);
                }
                else
                {
                    if (!interactionData.IsSameInteractable(_interactable))
                    {
                        interactionData.Interactable = _interactable;
                        uiIPanel.SetTooltip(_interactable.TooltipMessage);
                    }
                }
            }
        } 
        else
        {
            uiIPanel.ResetUI();
            interactionData.ResetData();
        }
        
        Debug.DrawRay(_ray.origin, _ray.direction, _hitSomething ? Color.green : Color.red);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void CheckForInteractable()
    {
        if (interactionData.IsEmpty)
            return;

        if (interactionInputData.InteractClick)
        {
            _minteracting = true;
            _mholdTimer = 0f;
        }

        if (interactionInputData.InteractReleased)
        {
            _minteracting = false;
            _mholdTimer = 0f;
            uiIPanel.UpdateProgressBar(_minteracting ? 1f : 0f);
        }

        if (_minteracting)
        {
            if (!interactionData.Interactable.IsInteractable)
                return;

            if (interactionData.Interactable.HoldInteract)
            {
                _mholdTimer += Time.deltaTime;

                float heldPercent = _mholdTimer / interactionData.Interactable.HoldDuration;
                uiIPanel.UpdateProgressBar(heldPercent);

                if (heldPercent >= 1f)
                {
                    interactionData.Interact();
                    _minteracting = false;
                }
            }
            else
            {
                interactionData.Interact();
                _minteracting = false;
            }
        }
    }
}
