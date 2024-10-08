using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffNeshJump : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 10.0f;
    [SerializeField]
    private float gravity = -9.8f;

    [SerializeField]
    private int offMexhAreaNumber = 2;

    private NavMeshAgent navAgent;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsOnJump());
            yield return StartCoroutine(JumpTo());
        }
    }

    IEnumerator JumpTo()
    {
        navAgent.isStopped = true;
        OffMeshLinkData linkData = navAgent.currentOffMeshLinkData;
        Vector3 start = transform.position;
        Vector3 end = linkData.endPos;

        float jumpTime = Mathf.Max(0.3f, Vector3.Distance(start, end) / jumpSpeed);
        float currenTime = 0;
        float percent = 0;

        float v0 = (end - start).y - gravity;   // y 방향 초기 속도

        while(percent < 1)
        {
            // 포물선 운동 : 시작위치 + 초기속도 * 시간 + 중력 * 시간의제곱
            currenTime += Time.deltaTime;
            percent = currenTime / jumpTime;

            Vector3 pos = Vector3.Lerp(start, end, percent);
            pos.y = start.y + (v0 * percent) + (gravity * percent * percent);

            transform.position = pos;
            yield return null;
        }

        navAgent.CompleteOffMeshLink();
        navAgent.isStopped = false;
    }

    private bool IsOnJump()
    {
        if (navAgent.isOnOffMeshLink)
        {
            OffMeshLinkData linkData = navAgent.currentOffMeshLinkData;

            if (linkData.offMeshLink != null && linkData.offMeshLink.area == offMexhAreaNumber)
            {
                return true;
            }
        }
        return false;
    }
}
