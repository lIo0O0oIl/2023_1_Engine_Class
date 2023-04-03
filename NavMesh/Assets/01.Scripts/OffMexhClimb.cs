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

    // �귡Ű��, ��ڻ�, ����
    private IEnumerator CliimbOrDescend()
    {
        navAgent.isStopped = true;  // �ϴ� �׺���̼��� �����.
        OffMeshLinkData linkData = navAgent.currentOffMeshLinkData;
        Vector3 start = linkData.startPos;
        Vector3 end = linkData.endPos;  // ����Ƽ�� �����ص� �������� �ƴϰ� �� ������ �� �ڽ��� ������

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

        navAgent.CompleteOffMeshLink();     // �����޽ø�ũ�� �� ���´�.
        navAgent.isStopped = false;     // ���� �ϰ� �̵��ض�...
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
