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

    private int currentCombo = 1;   // 현재 콤보가 몇인지
    private bool canAttack = true;      // 선입력 막기 위해서 다음 공격 가능상태인가
    private float keyTimer = 0;     // 다음 공격이 이루어지기 까지 기다리는 시간.
    // 이 시간내로 입력 안하면 idle 로 돌아간다.

    private float attackStartTime; // 공격이 시작된 시간을 기록하고
    [SerializeField]
    private float attackSlideDuration = 0.2f, attackSlideSpeed = 0.1f;
    // 슬라이드 되는 시간과 슬라이드 되는 스피드를 나타낸다.

    public override void OnEnterState()
    {
        agentInput.OnAttackKeyPress += OnAttackHandle;
        animator.OnAnimationEndTrigger += OnAnimationEnd;
        agentInput.OnRollingKeyPress += OnRollingHandle;
        currentCombo = 0;   // 초기화 하고
        canAttack = true;
        animator.SetAttackState(true);

        agentMovement.IsActiveMove = false;     // 키보드 이동을 잠그고
        Vector3 pos = agentInput.GetMouseWordPosition();
        agentMovement.SetRotation(pos);     // 마우스가 바라보는

        OnAttackHandle();   // 처음 1타
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

        agentMovement.IsActiveMove = true;  // 키보드 이동을 풀어주고

        OnAttackEnd?.Invoke();
    }

    private void OnAnimationEnd()
    {
        canAttack = true;
        keyTimer = keyDelay; // 0.5초 기다리기 시작
    }

    public void OnAttackHandle()
    {
        if (canAttack && currentCombo < 3)
        {
            attackStartTime = Time.time;
            canAttack = false;
            currentCombo++;
            // 애니메이션 트리거 해주기
            animator.SetAttackTrigger(true);

            OnAttackStart?.Invoke(currentCombo);    // 현재 콤보수치를 발행해준다.
        }
    }

    public override void UpdateState()
    {
        if (Time.time < attackStartTime + attackSlideDuration)  // 슬라이드가 되고 있어야 하는 시간
        {
            float timePassed = Time.time - attackStartTime;     // 현재 흘러간 시간이 나오고
            float lerpTime = timePassed / attackSlideDuration;  // 0~1값으로 맵핑하고

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
