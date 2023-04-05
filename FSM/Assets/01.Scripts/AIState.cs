using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ����
public class AIState : MonoBehaviour
{
    public List<AITransition> Transition = null;
    public List<AIAction> Actions = null;

    protected AIBrain brain;

    private void Awake()
    {
        brain = transform.GetComponentInParent<AIBrain>();
        if (brain == null)
        {
            Debug.LogError("���� �����");
        }

        Actions = new List<AIAction>();
        GetComponents<AIAction>(Actions);   // �ڱ⿡ �پ��ִ� ��� �׼��� ã�Ƽ� �־��ش�.
    }

    public void UpdateState()
    {
        // ���⼭ ���� �� ���¿��� ����� ������ �����ؾ� �Ѵ�.

        foreach(AIAction a in Actions)
        {
            a.TakeAction();
        }

        foreach(AITransition t in Transition)
        {
            if (t.CheckTransition())
            {
                // ���⼭ ������ȯ�� �ؾ��Ѵ�.
                brain.ChangeState(t.NextState);

                break;
            }
        }
    }
}
