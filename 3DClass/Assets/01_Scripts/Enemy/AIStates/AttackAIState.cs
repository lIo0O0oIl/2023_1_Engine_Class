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

        enemyController.AgentAnimator.SetAttackState(false);    // �ִϸ��̼� ���� 
        enemyController.AgentAnimator.SetAttackTrigger(false);
    }

    // ���� �ִϸ��̼��� ������ �� ó��
    private void AttackAnimationEndHandle()
    {
        enemyController.AgentAnimator.SetAttackState(false);
        aiActionData.IsAttacking = false;
    }

    private void AttackCollsionHandle()
    {
        // �̰� ���� �÷��̾� ü���� ���� ������ ������ �ȵ�.
    }

    public override void UpdateState()
    {
        base.UpdateState();     // ���� ������ ������ �Ÿ����� üũ���� ���ش�����

        if(aiActionData.IsAttacking == false)
        {
            SetTarget();    // Ÿ���� ���ϵ��� ���� ������ְ�
            // ������ ���� �����̼� ���ǵ忡 ���� ���ƾ� �ϴµ�

            Vector3 currentFrontVector = transform.forward;     // ĳ������ ��������
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
                enemyController.AgentAnimator.SetAttackTrigger(true);   // ���ݸ���� ����Ѵ�.
            }
            //Quaternion rot = Quaternion.LookRotation(targetVector);
            //transform.rotation = rot;
        }
    }

    private void SetTarget()
    {
        targetVector = enemyController.TargetTrm.position - transform.position;
        targetVector.y = 0;  // ���̴� ���ٰ� ����
    }
}
