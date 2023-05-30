using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimation : MonoBehaviour
{
    Animator animator;
    private readonly int walk = Animator.StringToHash("Walk");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Walk()
    {
        animator.SetBool(walk, true);     // �� �ȴ����̾�
    }

    public void NoWalk()
    {
        animator.SetBool(walk, false);    // �� �Ȱȴ� ���̾�
    }
}
