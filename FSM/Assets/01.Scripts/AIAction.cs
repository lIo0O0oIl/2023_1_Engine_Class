using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIBrain brain;
    protected AgentAnimation anim;

    protected virtual void Awake()
    {
        brain = GetComponentInParent<AIBrain>();
        anim = GetComponentInParent<AgentAnimation>();
    }

    public abstract void TakeAction();
}
