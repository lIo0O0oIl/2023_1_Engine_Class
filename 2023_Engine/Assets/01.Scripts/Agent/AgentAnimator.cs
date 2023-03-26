using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    private readonly int speedHash = Animator.StringToHash("speed");    // �콬�� 0���� 9������ �迭�� �ۼ�Ʈ�� �ؼ� �ۼ�Ʈ�� �� �迭�� ���� �ϴ� ��.
    // ���⼭�� ����� speed�� int ������ �ٲ㼭 ����Ѱ� �׷��� ���ڿ��� �ڵ�(����)������ ����(�ؽ�) int������...��¼��..
    // �� �ؽ��� �ܹ�����. �׷��� �α����� ���� �ؽ��� �Է°��� ���ϴ°���.
    private readonly int isAirgoneHash = Animator.StringToHash("is_airbone");
    private readonly int attackHash = Animator.StringToHash("attack");
    private readonly int isAttackHash = Animator.StringToHash("is_attack");
    private readonly int isRollingHash = Animator.StringToHash("is_rolling");

    public event Action OnAnimationEndTrigger = null;

    private Animator animator;
    public Animator Animator => animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetRollingState(bool value)
    {
        animator.SetBool(isRollingHash, value);
    }

    public void SetSpeed(float value)
    {
        animator.SetFloat(speedHash, value);
    }

    public void SetAirbone(bool value)
    {
        animator.SetBool(isAirgoneHash, value);
    }

    public void SetAttackState(bool value)
    {
        animator.SetBool(isAttackHash, value);
    }

    public void SetAttackTrigger(bool value)
    {
        if (value)
        {
            animator.SetTrigger(attackHash);
        }
        else
        {
            animator.ResetTrigger(attackHash);
        }
    }

    public void OnAnimationEnd()
    {
        OnAnimationEndTrigger?.Invoke();
    }
}
