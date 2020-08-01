using UnityEngine;

namespace ProjectX
{
    public class DoorOpenInteractable : InteractableBase
    {
        [SerializeField] private Animator anim;
        private bool isOpen = true;
        public override void OnInteract()
        {
            OpenDoor();
        }

        private void OpenDoor()
        {
            anim.SetBool("isOpen", isOpen);
            isOpen = !isOpen;
        }
    }
}