using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> Decisions;
    public CommonAIState NextState;

    public void SetUp(Transform enemyRoot)
    {
        Decisions.ForEach(d => d.SetUp(enemyRoot));
    }

    public bool CheckTransition()
    {
        bool result = false;
        foreach(AIDecision decition in Decisions)
        {
            result = decition.MakeDecision();
            if (decition.IsReverse)
            {
                result = !result;
            }
            if (result == false)
            {
                break;
            }
        }
        return result;
    }
}
