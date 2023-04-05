using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    private MeshRenderer renderer;
    protected override void Awake()
    {
        base.Awake();
        renderer = brain.GetComponent<MeshRenderer>();
    }

    public override void TakeAction()
    {
        // do nothing;
        brain.SetDestination(brain.TragetTrm.position);
        renderer.material.color = new Color(1, 0, 0);   // 빨간색 컬러로 셋
    }
}
