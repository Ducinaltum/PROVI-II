using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour, IPoolable
{
    private static ObjectPool<T> m_instance;
    public static ObjectPool<T> Instance => m_instance ??= new();

    public Queue<T> m_objectcs = new();
    public ObjectPool() { }

    public T GetObject()
    {
        if (m_objectcs.Count == 0)
        {
            return null;
        }
        else
        {
            T obj;
            do
                obj = m_objectcs.Dequeue();
            while (obj == null && m_objectcs.Count > 0);

#if UNITY_EDITOR
            //A object that is in the pool shoudnt be active
            if (obj.gameObject.activeSelf)
            {
                Debug.Log(obj.gameObject.activeSelf, obj.gameObject);
                Debug.Break();
            }
#endif
            return obj;
        }
    }

    public void DisposeObject(T obj)
    {
        obj.gameObject.SetActive(false);
        m_objectcs.Enqueue(obj);
    }

    public void ClearPool()
    {
        m_objectcs.Clear();
    }
}


