using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExaminable
{
    ExaminableBase  Prepare();
    void Drag(float speed);

    void Use();
}
