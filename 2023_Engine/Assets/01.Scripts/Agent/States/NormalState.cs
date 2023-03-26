using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : CommonState
{
    //protected AgentMovement agentMovement;

    public override void OnEnterState()
    {
        agentMovement?.Stoplmmediately();
        agentInput.OnMovementKeyPress += OnMovementHandle;
        agentInput.OnAttackKeyPress += OnAttackKeyHandle;
        agentInput.OnRollingKeyPress += OnRollingHandle;
    }

    public override void OnExitState()
    {
        agentMovement?.Stoplmmediately();
        agentInput.OnMovementKeyPress -= OnMovementHandle;
        agentInput.OnAttackKeyPress -= OnAttackKeyHandle;
        agentInput.OnRollingKeyPress -= OnRollingHandle;
    }

    private void OnMovementHandle(Vector3 obj)
    {
        agentMovement?.SetMovementVelocity(obj);
    }

    private void OnAttackKeyHandle()
    {
        Vector3 targetPos = agentInput.GetMouseWordPosition();
        //agentMovement.SetRotation(targetPos);     // ���⼭ ���콺 �������� ȸ�� �ϴ� �� �����
        agentController.ChangeState(StateType.Attack);
    }

    private void OnRollingHandle()
    {
        agentController.ChangeState(StateType.Rolling);
    }

    /*public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        agentMovement = agentRoot.GetComponent<AgentMovement>();
    }*/

    public override void UpdateState()
    {

    }
}