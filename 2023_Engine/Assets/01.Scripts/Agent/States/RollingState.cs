using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingState : CommonState
{
    [SerializeField] private float rollingSpeed = 0.4f, animationThreshold = 0.1f;  // �ִϸ��̼� ������Ȧ��(�Ӱ���)

    private float timer = 0;

    public override void OnEnterState()
    {
        animator.OnAnimationEndTrigger += RollingEngdHandle;
        animator.SetRollingState(true);
        agentMovement.IsActiveMove = false;
        // ���⼭ ��� ������ ���� ȸ����ų�� �����ؾ� �Ѵ�. ������ ���콺�� ����
        /*Vector3 mousePos = agentInput.GetMouseWordPosition();
        Vector3 dir = mousePos - agentController.transform.position;

        agentMovement.SetRotation(mousePos);*/
        //agentMovement.SetMovementVelocity(dir.normalized * rollingSpeed);

        Vector3 keyDir = agentInput.GetCurrentInputDirection();
        // ���⼭ �޾Ҵµ� ���� Ű�� �ȴ����� �־��ٸ� ���� �ٶ󺸴� �������� ��������
        if (keyDir.magnitude < 0.1f)
        {
            keyDir = agentController.transform.forward;
        }
        agentMovement.SetRotation(keyDir + agentController.transform.position);
        agentMovement.SetMovementVelocity(keyDir.normalized * rollingSpeed);
        timer = 0;  // Ÿ�̸� ����
    }

    private void RollingEngdHandle()
    {
        if (timer < animationThreshold) return;

        agentMovement.Stoplmmediately();
        agentController.ChangeState(StateType.Normal);
    }

    public override void OnExitState()
    {
        animator.OnAnimationEndTrigger -= RollingEngdHandle;
        agentMovement.IsActiveMove = true;
        animator.SetRollingState(false);
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;
    }
}
