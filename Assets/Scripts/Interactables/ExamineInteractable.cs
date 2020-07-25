using System.Collections;
using System.Collections.Generic;
using ProjectX;
using UnityEngine;

public class ExamineInteractable : InteractableBase
{
    public override void OnInteract()
    {
        base.OnInteract();
        ExamineController.Instance.Examine(this);
    }
    
}
