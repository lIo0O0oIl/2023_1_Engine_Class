using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNavAgent : MonoBehaviour
{
    [SerializeField]
    private Transform targetTrm;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Vector3 pos = targetTrm.position;
            pos.y = transform.position.y;
            agent.SetDestination(pos);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos;
            if (GameManager.Instance.GetMouseWorldPosition(out pos))
            {
                agent.SetDestination(pos);
            }
        }
    }
}
