using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    private Dictionary<string, Pool<PoolableMono>> pools = new Dictionary<string, Pool<PoolableMono>>();

    private Transform trmParent;
    public PoolManager(Transform trmParent)
    {
        this.trmParent = trmParent;
    }

    public void CreatePool(PoolableMono perfab, int count = 10)
    {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(perfab, trmParent, count);
        pools.Add(perfab.gameObject.name, pool);    // 프리팹의 이름으로 풀을 만든다.
    }

    public PoolableMono Pop(string perfaName)
    {
        if (!pools.ContainsKey(perfaName))
        {
            Debug.LogError($"이건 없어! {perfaName}");
            return null;
        }

        PoolableMono item = pools[perfaName].Pop();
        item.Reset();
        return item;
    }

    public void Push(PoolableMono obj)
    {
        pools[obj.name].Push(obj);
    }
}
