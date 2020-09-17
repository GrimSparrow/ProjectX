using System;
using UnityEngine;

namespace ProjectX
{    
    public class InteractionController : MonoBehaviour
    {
        #region Variables    
            [Space, Header("Data")]
            [SerializeField] private InteractionInputData interactionInputData = null;
            [SerializeField] private InteractionData interactionData = null;
            [SerializeField] private InteractionSprites interactionSprites;

            [Space, Header("UI")]
            [SerializeField] private InteractionUIPanel uiPanel;

            [Space, Header("Ray Settings")]
            [SerializeField] private float rayDistance = 0f;
            [SerializeField] private float raySphereRadius = 0f;
            [SerializeField] private LayerMask interactableLayer = ~0;


            #region Private
                private Camera m_cam;

                private bool m_interacting;
                private float m_holdTimer = 0f;
                
            #endregion

        #endregion

        #region Built In Methods      
            void Awake()
            {
                m_cam = FindObjectOfType<Camera>();
            }

            void Update()
            {
                if (!ExamineController.Instance.IsExamine)
                {
                    CheckForInteractable();
                    CheckForInteractableInput();
                }
            }
        #endregion


        #region Custom methods         
            void CheckForInteractable()
            {
                Ray _ray = new Ray(m_cam.transform.position,m_cam.transform.forward);
                RaycastHit _hitInfo;

                bool _hitSomething = Physics.SphereCast(_ray,raySphereRadius, out _hitInfo, rayDistance, interactableLayer);

                if(_hitSomething)
                {
                    InteractableBase _interactable = _hitInfo.transform.GetComponent<InteractableBase>();

                    if(_interactable != null && !ExamineController.Instance.IsExamine)
                    {
                        if(interactionData.IsEmpty())
                        {
                            interactionData.Interactable = _interactable;
                            GetItemType(_interactable);
                        }
                        else
                        {
                            if(!interactionData.IsSameInteractable(_interactable))
                            {
                                interactionData.Interactable = _interactable;
                                GetItemType(_interactable);
                            }  
                        }
                    }
                }
                else
                {
                    uiPanel.ResetUI();
                    interactionData.ResetData();
                }

                Debug.DrawRay(_ray.origin,_ray.direction * rayDistance,_hitSomething ? Color.green : Color.red);
            }

            public enum CrosshairTypes
            {
                Default,
                Take,
                Examine
            }

            void  GetItemType(InteractableBase item)
            {
                if (!item.IsInteractable)
                {
                    SetCrosshair(CrosshairTypes.Default);
                    return;
                }
                
                switch (item)
                {
                    case SimpleExaminable _:
                    case NoteExaminable _:
                        SetCrosshair(CrosshairTypes.Examine);
                        break;
                    case ExaminableItem _:
                    case DoorOpenInteractable _: 
                    case RotateObject _:
                        SetCrosshair(CrosshairTypes.Take);
                        break;
                    default:
                        SetCrosshair(CrosshairTypes.Default);
                        break;
                }
            }

            void SetCrosshair(CrosshairTypes type)
            {
                switch (type)
                {
                    case CrosshairTypes.Default:
                        uiPanel.SetCrosshair(interactionSprites.Sprites[0]);
                        break;
                    case CrosshairTypes.Take:
                        uiPanel.SetCrosshair(interactionSprites.Sprites[1]);
                        break;
                    case CrosshairTypes.Examine:
                        uiPanel.SetCrosshair(interactionSprites.Sprites[2]);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
                
            }

            void CheckForInteractableInput()
            {
                if(interactionData.IsEmpty())
                    return;

                if(interactionInputData.InteractedClicked)
                {
                    m_interacting = true;
                    m_holdTimer = 0f;
                }

                if(interactionInputData.InteractedReleased)
                {
                    m_interacting = false;
                    m_holdTimer = 0f;
                    uiPanel.UpdateProgressBar(0f);
                }

                if(m_interacting)
                {
                    if(!interactionData.Interactable.IsInteractable)
                        return;

                    if(interactionData.Interactable.HoldInteract)
                    {
                        m_holdTimer += Time.deltaTime;

                        float heldPercent = m_holdTimer / interactionData.Interactable.HoldDuration;
                        uiPanel.UpdateProgressBar(heldPercent);

                        if(heldPercent > 1f)
                        {
                            interactionData.Interact();
                            m_interacting = false;
                        }
                    }
                    else
                    {
                        interactionData.Interact();
                        m_interacting = false;
                    }
                }
            }
        #endregion
    }
}
