using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffMexhClimb : MonoBehaviour
{
    [SerializeField]
    private int offMexhAreaNumber = 4;
    [SerializeField]
    private float climbSpeed = 1.5f;
    private NavMeshAgent navAgent;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    IEnumerator Start()
    {
        while(true)
        {
            yield return new WaitUntil(() => IsOnClimb());
            yield return StartCoroutine(CliimbOrDescend());
        }
    }

    // 브래키스, 고박사, 베르
    private IEnumerator CliimbOrDescend()
    {
        navAgent.isStopped = true;  // 일단 네비게이션을 멈춘다.
        OffMeshLinkData linkData = navAgent.currentOffMeshLinkData;
        Vector3 start = linkData.startPos;
        Vector3 end = linkData.endPos;  // 유니티에 연결해둔 포지션이 아니고 딱 시작할 때 자신의 정보임

        float climbTime = Mathf.Abs(end.y - start.y) / climbSpeed;
        float currentTime = 0;
        float percent = 0;

        while(percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / climbTime;

            transform.position = Vector3.Lerp(start, end, percent);
            yield return null;
        }

        navAgent.CompleteOffMeshLink();     // 오프메시링크를 다 끝냈다.
        navAgent.isStopped = false;     // 이제 니가 이동해라...
    }
    
    private bool IsOnClimb()
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
