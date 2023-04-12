using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoolingItem
{
    public PoolableMono Perfab;
    public int Count;
}

[CreateAssetMenu (menuName ="SO/PooingList")]
public class PoolingListSO : ScriptableObject
{

}
