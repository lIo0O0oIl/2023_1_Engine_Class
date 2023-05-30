using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleIsHere : AIDecision
{
    public Transform PlayerTrm;
    public float Distance = 5f;
    public LayerMask obstacle;

    public override bool MakeDecision()
    {
        if( Vector3.Distance(PlayerTrm.position, transform.position) < Distance)
        {
            Vector3 dir = PlayerTrm.position - transform.position;
            if (Physics.Raycast(transform.position, dir, Distance, obstacle)) {
                Debug.Log("��ֹ��� ����");
                return false;
            }
            else
            {
                Debug.Log("��ֹ��� ����");
                return true;
            }
        }
        return false;
    }
}
