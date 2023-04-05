using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//현재 상태
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
            Debug.LogError("뇌가 없어요");
        }

        Actions = new List<AIAction>();
        GetComponents<AIAction>(Actions);   // 자기에 붙어있는 모든 액션을 찾아서 넣어준다.
    }

    public void UpdateState()
    {
        // 여기서 원래 내 상태에서 해줘야 할일을 수행해야 한다.

        foreach(AIAction a in Actions)
        {
            a.TakeAction();
        }

        foreach(AITransition t in Transition)
        {
            if (t.CheckTransition())
            {
                // 여기서 상태전환을 해야한다.
                brain.ChangeState(t.NextState);

                break;
            }
        }
    }
}
