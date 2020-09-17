using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Note", menuName = "Story/Data/Create Note")]
[Serializable]
public class Note : ScriptableObject
{
    [TextArea] public string Text;
    public Sprite Template;
}
