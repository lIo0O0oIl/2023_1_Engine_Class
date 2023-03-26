using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CommonState
{
    public event Action<int> OnAttackStart = null;
    public event Action OnAttackEnd = null;

    [SerializeField]
    private float keyDelay = 0.5f;

    private int currentCombo = 1;   // ���� �޺��� ������
    private bool canAttack = true;      // ���Է� ���� ���ؼ� ���� ���� ���ɻ����ΰ�
    private float keyTimer = 0;     // ���� ������ �̷������ ���� ��ٸ��� �ð�.
    // �� �ð����� �Է� ���ϸ� idle �� ���ư���.

    private float attackStartTime; // ������ ���۵� �ð��� ����ϰ�
    [SerializeField]
    private float attackSlideDuration = 0.2f, attackSlideSpeed = 0.1f;
    // �����̵� �Ǵ� �ð��� �����̵� �Ǵ� ���ǵ带 ��Ÿ����.

    public override void OnEnterState()
    {
        agentInput.OnAttackKeyPress += OnAttackHandle;
        animator.OnAnimationEndTrigger += OnAnimationEnd;
        agentInput.OnRollingKeyPress += OnRollingHandle;
        currentCombo = 0;   // �ʱ�ȭ �ϰ�
        canAttack = true;
        animator.SetAttackState(true);

        agentMovement.IsActiveMove = false;     // Ű���� �̵��� ��װ�
        Vector3 pos = agentInput.GetMouseWordPosition();
        agentMovement.SetRotation(pos);     // ���콺�� �ٶ󺸴�

        OnAttackHandle();   // ó�� 1Ÿ
    }

    private void OnRollingHandle()
    {
        agentController.ChangeState(StateType.Rolling);
    }

    public override void OnExitState()
    {
        agentInput.OnAttackKeyPress -= OnAttackHandle;
        animator.OnAnimationEndTrigger -= OnAnimationEnd;
        agentInput.OnRollingKeyPress -= OnRollingHandle;
        animator.SetAttackTrigger(false);
        animator.SetAttackState(false);

        agentMovement.IsActiveMove = true;  // Ű���� �̵��� Ǯ���ְ�

        OnAttackEnd?.Invoke();
    }

    private void OnAnimationEnd()
    {
        canAttack = true;
        keyTimer = keyDelay; // 0.5�� ��ٸ��� ����
    }

    public void OnAttackHandle()
    {
        if (canAttack && currentCombo < 3)
        {
            attackStartTime = Time.time;
            canAttack = false;
            currentCombo++;
            // �ִϸ��̼� Ʈ���� ���ֱ�
            animator.SetAttackTrigger(true);

            OnAttackStart?.Invoke(currentCombo);    // ���� �޺���ġ�� �������ش�.
        }
    }

    public override void UpdateState()
    {
        if (Time.time < attackStartTime + attackSlideDuration)  // �����̵尡 �ǰ� �־�� �ϴ� �ð�
        {
            float timePassed = Time.time - attackStartTime;     // ���� �귯�� �ð��� ������
            float lerpTime = timePassed / attackSlideDuration;  // 0~1������ �����ϰ�

            Vector3 targetSpeed = Vector3.Lerp(agentController.transform.forward * attackSlideSpeed, Vector3.zero, lerpTime);
            agentMovement.SetMovementVelocity(targetSpeed);
        }

        if (canAttack && keyTimer > 0)
        {
            keyTimer -= Time.deltaTime;
            if (keyTimer <= 0)
            {
                agentController.ChangeState(StateType.Normal);
            }
        }
    }
}
