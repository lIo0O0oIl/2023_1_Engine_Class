using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunBehaviour : StateMachineBehaviour
{
    private PlayerVFXManager vfsManager = null;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (vfsManager == null)
        {
            vfsManager = animator.transform.parent.GetComponent<PlayerVFXManager>();
        }
        vfsManager?.UpdateFootStep(true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        vfsManager?.UpdateFootStep(false);
    }
}
