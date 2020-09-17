using UnityEngine;
using NaughtyAttributes;

namespace ProjectX
{
    public class InteractableBase : MonoBehaviour, IInteractable
    {
        #region Variables

        [Space, Header("Quest Settings")] 
        
        public TaskBaseNode TaskToComplete;
            
        [Space,Header("Interactable Settings")]

        [SerializeField] private bool holdInteract = true;
        [ShowIf("holdInteract")][SerializeField] private float holdDuration = 1f;
        
        [Space] 
        [SerializeField] private bool multipleUse = false;
        [SerializeField] private bool isInteractable = true;

        [SerializeField] [TextArea] private string SayText;
        #endregion

        #region Properties    
        public float HoldDuration => holdDuration; 

        public bool HoldInteract => holdInteract;
        public bool MultipleUse => multipleUse;
        public bool IsInteractable
        {
            get => isInteractable;
            set => isInteractable = value;
        }

        #endregion

        #region Methods
        public virtual void OnInteract()
            {
            }

        public virtual void CantInteract()
        {
            NotificationManager.Instance.SetNotification(SayText);
        }

        #endregion
    }
}
