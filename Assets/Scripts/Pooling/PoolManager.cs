using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{

    public List<Transform> m_enemyObjects;
    public List<Transform> m_playerObjects;

    private List<ObjectPool> pools;

    private void Awake()
    {
        pools = new List<ObjectPool>();
    }

    // a new object type is requested so a new pool has to be created
    // pools are child of pool manager
    private ObjectPool CreatePool(Transform prefab)
    {
        var obj = new GameObject(prefab.name + " Pool");
        var tmp = obj.AddComponent<ObjectPool>();
        tmp.SetPrefab(prefab);

        //Fails if the script isnt assigned to the prefab
        prefab.GetComponent<IPoolable>().m_pool = tmp;
        pools.Add(tmp);
        tmp.transform.parent = transform;
        obj.transform.position = Vector3.zero;
        return tmp;
    }

    public IPoolable GetObject(Transform objectPrefab, Vector3 pos, Quaternion rot)
    {
        for (var i = 0; i < pools.Count; ++i)
        {
            if (pools[i].IsType(objectPrefab))
                return pools[i].Request(pos, rot);
        }

        var tmp = CreatePool(objectPrefab);
        return tmp.Request(pos, rot);
    }
}
