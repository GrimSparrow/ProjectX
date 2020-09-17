using ProjectX;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RotateObject : InteractableBase
{
    private Animator animator;
    private AudioSource audio;

    [SerializeField] private AudioClip moveSound;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    public override void OnInteract()
    {
        MoveObject();
    }

    public void PlaySound()
    {
        audio.PlayOneShot(moveSound);
    }
    
    private void MoveObject()
    {
        animator.Play("Rotate");
        IsInteractable = false;
    }
}
