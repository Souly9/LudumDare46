using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{

    private Stack<IPoolable> m_objects;
    private Transform m_pooledObject;

    public void SetPrefab(Transform objectPrefab)
    {
        m_objects = new Stack<IPoolable>();
        m_pooledObject = objectPrefab;
    }

    internal bool IsType(Transform objectPrefab)
    {
        return m_pooledObject == objectPrefab;
    }

    internal IPoolable Request(Vector3 pos, Quaternion rot)
    {
        IPoolable tmp;
        if (m_objects.Count > 0)
        {
            tmp = m_objects.Pop();
        }
        else
        {
            tmp = Instantiate(m_pooledObject).GetComponent<IPoolable>();
            tmp.m_pool = this;
            tmp.m_transform.parent = transform;
        }

        tmp.m_transform.position = pos;
        tmp.Init();
        tmp.m_gameObject.SetActive(true);
        return tmp;
    }

    public void Return(IPoolable obj)
    {
        obj.m_gameObject.SetActive(false);
        m_objects.Push(obj);
    }
}
