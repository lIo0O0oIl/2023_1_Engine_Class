using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    private Dictionary<StateType, IState> stateDictionary = null;  //������ �ִ� ���µ� ����
    private IState currentState; //���� ���� ����

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
        currentState?.OnExitState(); //���� ���� ������
        currentState = stateDictionary[type];
        currentState?.OnEnterState(); //�������� ����
    }

    private void Update()
    {
        currentState.UpdateState();
    }
}
