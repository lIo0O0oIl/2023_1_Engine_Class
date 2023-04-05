using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDistanceDecision : AIDecision
{
    [SerializeField]
    private float _distance = 5f;

    [SerializeField]
    private bool isAlwaysVisible = false;   // 디버깅 용도

    public override bool MakeDecision()
    {
        if (enemyController.TargetTrm == null) return false;

        float distance = Vector3.Distance(enemyController.TargetTrm.position, transform.position);

        if (distance < _distance)   // 시야 안으로 들어왔으니 추적 시작
        {
            aiActionData.LastSoptPoint = enemyController.TargetTrm.position;    // 마지막으로 본 시점으로 기록
            aiActionData.TargetSpotted = true;  // 적을 발견했다.
        }
        else
        {
            aiActionData.TargetSpotted = false;
        }
        return aiActionData.TargetSpotted;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject || isAlwaysVisible == true)
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _distance);
            Gizmos.color = oldColor;
        }
    }
#endif
}
