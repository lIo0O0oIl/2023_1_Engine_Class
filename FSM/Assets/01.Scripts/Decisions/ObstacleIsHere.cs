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
                Debug.Log("장애물에 닿음");
                return false;
            }
            else
            {
                Debug.Log("장애물이 없음");
                return true;
            }
        }
        return false;
    }
}
