using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonState : MonoBehaviour, IState
{
    public abstract void OnEnterState();
    public abstract void OnExitState();
    public abstract void UpdateState();

    protected AgentAnimator animator;
    protected AgentInput agentInput;
    protected AgentController agentController;
    protected AgentMovement agentMovement;  // 이동관련

    public virtual void SetUp(Transform agentRoot)
    {
        animator = agentRoot.Find("Visual").GetComponent<AgentAnimator>();
        agentInput = agentRoot.GetComponent<AgentInput>();
        agentController = agentRoot.GetComponent<AgentController>();
        agentMovement = agentRoot.GetComponent<AgentMovement>();
    }

    // 피격처리시 사용할 것
    public void OnHitHandle(Vector3 hitPoint, Vector3 Normal)
    {

    }
}
