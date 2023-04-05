using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 조건 안에 어쩌고
public abstract class AIDecision : MonoBehaviour
{
    public bool IsReverse = false;
    public abstract bool MakeDecision();
}
