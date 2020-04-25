using UnityEngine;
using System.Collections;

public interface IPoolable 
{

    ObjectPool m_pool { get; set; }

    GameObject m_gameObject { get; }

    Transform m_transform { get; }

    // interface t o initialize the object in case it needs special care
    void Init();
}
