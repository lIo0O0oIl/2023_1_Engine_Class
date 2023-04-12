using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAIState : CommonAIState
{
    [SerializeField] private float rotateSpeed = 720f;

    protected Vector3 targetVector;

    public override void OnEnterState()
    {
        enemyController.NavMovement.StopImmediately();
        enemyController.AgentAnimator.OnAnimationEventTrigger += AttackAnimationEndHandle;
        enemyController.AgentAnimator.OnAnimationEventTrigger += AttackCollsionHandle;
        aiActionData.IsAttacking = false;
    }

    public override void OnExitState()
    {
        enemyController.AgentAnimator.OnAnimationEventTrigger -= AttackCollsionHandle;
        enemyController.AgentAnimator.OnAnimationEventTrigger -= AttackAnimationEndHandle;

        enemyController.AgentAnimator.SetAttackState(false);    // 애니메이션 리셋 
        enemyController.AgentAnimator.SetAttackTrigger(false);
    }

    // 공격 애니메이션이 끝났을 때 처리
    private void AttackAnimationEndHandle()
    {
        enemyController.AgentAnimator.SetAttackState(false);
        aiActionData.IsAttacking = false;
    }

    private void AttackCollsionHandle()
    {
        // 이건 아직 플레이어 체력이 없기 때문에 공격이 안됨.
    }

    public override void UpdateState()
    {
        base.UpdateState();     // 먼저 공격이 가능한 거리인지 체크부터 해준다음에

        if(aiActionData.IsAttacking == false)
        {
            SetTarget();    // 타겟을 향하도록 백터 만들어주고
            // 여서기 원래 로테이션 스피드에 맞춰 돌아야 하는데

            Vector3 currentFrontVector = transform.forward;     // 캐릭터의 전방으로
            float angle = Vector3.Angle(currentFrontVector, targetVector);

            if (angle >= 10f)
            {
                Vector3 result = Vector3.Cross(currentFrontVector, targetVector);

                float sign = result.y > 0 ? 1 : -1;
                enemyController.transform.rotation = Quaternion.Euler
                    (0, sign * rotateSpeed * Time.deltaTime, 0) * enemyController.transform.rotation;
            }
            else
            {
                aiActionData.IsAttacking = true;
                enemyController.AgentAnimator.SetAttackState(true);
                enemyController.AgentAnimator.SetAttackTrigger(true);   // 공격모션을 재생한다.
            }
            //Quaternion rot = Quaternion.LookRotation(targetVector);
            //transform.rotation = rot;
        }
    }

    private void SetTarget()
    {
        targetVector = enemyController.TargetTrm.position - transform.position;
        targetVector.y = 0;  // 높이는 없다고 보고
    }
}
