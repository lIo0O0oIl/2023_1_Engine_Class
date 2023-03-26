using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    private Dictionary<StateType, IState> stateDictionary = null;  //가지고 있는 상태들 저장
    private IState currentState; //현재 상태 저장

    private void Awake()
    {
        stateDictionary = new Dictionary<StateType, IState>();
        Transform stateTrm = transform.Find("States");

        foreach (StateType state in Enum.GetValues(typeof(StateType)))
        {
            IState stateScript = stateTrm.GetComponent($"{state}State") as IState;
            if (stateScript == null)
            {
                Debug.LogError($"There is no script : {state}");
                return;
            }
            stateScript.SetUp(transform);
            stateDictionary.Add(state, stateScript);
        }
    }
    private void Start()
    {
        ChangeState(StateType.Normal);
    }

    public void ChangeState(StateType type)
    {
        currentState?.OnExitState(); //현재 상태 나가고
        currentState = stateDictionary[type];
        currentState?.OnEnterState(); //다음상태 시작
    }

    private void Update()
    {
        currentState.UpdateState();
    }
}
