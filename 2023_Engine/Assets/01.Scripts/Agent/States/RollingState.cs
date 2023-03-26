using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingState : CommonState
{
    [SerializeField] private float rollingSpeed = 0.4f, animationThreshold = 0.1f;  // 애니메이션 쓰레스홀드(임계점)

    private float timer = 0;

    public override void OnEnterState()
    {
        animator.OnAnimationEndTrigger += RollingEngdHandle;
        animator.SetRollingState(true);
        agentMovement.IsActiveMove = false;
        // 여기서 어느 방향을 보고 회전시킬지 결정해야 한다. 지금은 마우스를 보기
        /*Vector3 mousePos = agentInput.GetMouseWordPosition();
        Vector3 dir = mousePos - agentController.transform.position;

        agentMovement.SetRotation(mousePos);*/
        //agentMovement.SetMovementVelocity(dir.normalized * rollingSpeed);

        Vector3 keyDir = agentInput.GetCurrentInputDirection();
        // 여기서 받았는데 만약 키를 안누르고 있었다면 지금 바라보는 방향으로 돌링들어가기
        if (keyDir.magnitude < 0.1f)
        {
            keyDir = agentController.transform.forward;
        }
        agentMovement.SetRotation(keyDir + agentController.transform.position);
        agentMovement.SetMovementVelocity(keyDir.normalized * rollingSpeed);
        timer = 0;  // 타이머 시작
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
