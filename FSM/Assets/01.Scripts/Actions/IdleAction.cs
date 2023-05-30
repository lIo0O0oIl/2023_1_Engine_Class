using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    private MeshRenderer renderer;
    protected override void Awake()
    {
        base.Awake();
        //renderer = brain.GetComponent<MeshRenderer>();
    }

    public override void TakeAction()
    {
        // do nothing;
        Debug.Log("걷고있니..?");
        anim.NoWalk();
        //renderer.material.color = new Color(0, 0, 1);   // 파란색 컬러로 셋
    }
}
