using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �ȿ� ��¼��
public abstract class AIDecision : MonoBehaviour
{
    public bool IsReverse = false;
    public abstract bool MakeDecision();
}
