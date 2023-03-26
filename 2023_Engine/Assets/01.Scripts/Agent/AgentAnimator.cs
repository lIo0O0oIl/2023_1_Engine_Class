using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    private readonly int speedHash = Animator.StringToHash("speed");    // 헤쉬는 0부터 9까지의 배열을 퍼센트로 해서 퍼센트당 또 배열을 만들어서 하는 것.
    // 여기서의 사용은 speed를 int 형으로 바꿔서 사용한것 그래서 문자열을 코드(숫자)값으로 만들어서(해슁) int형으로...어쩌구..
    // 또 해쉬는 단방향임. 그래서 로그인할 때는 해슁과 입력값을 비교하는것임.
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
