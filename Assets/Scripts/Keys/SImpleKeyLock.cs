using System.Collections;
using ProjectX;
using UnityEngine;

public class SImpleKeyLock : BaseLockContainer
{
    [SerializeField] private PrefabSourceContainer key;
    private Animator animator;
    [SerializeField] private DoorOpenInteractable _interactable;
    private bool isOpemed = false;
    
    public override bool CanOpen()
    {
        animator = GetComponent<Animator>();
        return false;
    }

    public void Open()
    {
        _interactable.OpenDoor();
    }
}