using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public bool isAttack = false;

    public override void TakeAction()
    {
        isAttack = true;
    }
}
