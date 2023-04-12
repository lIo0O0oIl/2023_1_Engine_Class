using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolableMono
{
    private Stack<T> pool = new Stack<T>();
    private T perfab;   // 오리지날 프리펩 저장해두기
    private Transform parent;

    public Pool(T perfab, Transform parent, int count = 10)
    {
        this.perfab = perfab;
        this.parent = parent;

        for (int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(perfab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(clone)", "");
            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }

    public T Pop()
    {
        T obj = null;
        if (pool.Count <= 0)
        {
            obj = GameObject.Instantiate(perfab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            obj = pool.Pop();
            obj.gameObject.SetActive(true);
        }
        return obj;
    }

    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Push(obj);
    }
}
