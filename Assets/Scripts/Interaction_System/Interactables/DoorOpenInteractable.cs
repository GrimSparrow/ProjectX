using UnityEngine;

namespace ProjectX
{
    public class DoorOpenInteractable : InteractableBase
    {
        private Animator doorAnim;
        [SerializeField]private bool isOpen;
        
        
        [SerializeField] private bool isOpened;
        [SerializeField] private PrefabSourceContainer key;
        [SerializeField] private GameObject LOCK_OBJ;
        [Header("Unlock sounds")]
        [SerializeField] private AudioClip locked;
        [SerializeField] private AudioClip unlocked;
        [Header("Door sounds")]
        [SerializeField] private AudioClip open;
        [SerializeField] private AudioClip close;
        
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            doorAnim = GetComponent<Animator>();
        }

        public override void OnInteract()
        {
            TryOpen();
        }

        private void PlayLockedSound()
        {
            _audioSource.PlayOneShot(locked);
        }

        public void PlayOpenDoorSound()
        {
            _audioSource.PlayOneShot(open);
        }

        public void PlayCloseDoorSound()
        {
            _audioSource.PlayOneShot(close);
        }

        public void PlayUnlockSound()
        {
            _audioSource.PlayOneShot(unlocked);
        }

        public void SetKeyActive()
        {
            LOCK_OBJ.SetActive(true);
        }
        
        public void SetKeyInActive()
        {
            LOCK_OBJ.SetActive(false);
        }

        public void OpenDoor()
        {
            doorAnim.SetBool("isOpen", isOpen);
            isOpen = !isOpen;
        }
        
        public bool TryOpen()
        {
            if (isOpened || key == null)
            {
                OpenDoor();
                return isOpened;
            }
            
            if (InventoryController.Instance.UseItem(key))
            {
                doorAnim.Play("Unlock");
                OpenDoor();
                isOpened = true;
                QuestController.Instance.MoveToNextTask(TaskToComplete);
            }
            else
            {
                CantInteract();
                PlayLockedSound();
            }
            return isOpened;
        }
        
    }
}