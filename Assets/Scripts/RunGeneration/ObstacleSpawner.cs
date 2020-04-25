using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.UIElements;
using System;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class ObstacleSpawner : MonoBehaviour
{
    //variables to control difficulty of the level
    [Tooltip("Maximum number of active obstacles")]
    public int m_maxObstacles;

    public float m_velocity { get; set; }
    public float m_randVelocity { get; set; }

    public List<SpawnableObstacle> m_obstaclePrefabs;

    private List<SpawnableObstacle> m_activeObstacles;

    //start of the spawnline and the y length of it
    private Vector3 m_spawnLineBegin;
    private float lineLength;

    //spawn delay variables
    public float m_delayBetweenSpawn { get; set; }
    private float m_currentDelay;

    public float m_slowdownVel { get; set; }

    private PlayerManager m_player;

    public PoolManager m_poolManager;

    // Use this for initialization
    void Awake()
    {
        m_activeObstacles = new List<SpawnableObstacle>();
        m_poolManager = FindObjectOfType<PoolManager>();
        BoxCollider col = GetComponent<BoxCollider>();
        Vector3 ext = col.size;

        // Setup the length of the spawn line
        //-----------------------------------------------------------
        //make the line centered without x/z offset
        ext.x = ext.z = 0;
        //the spawner is in the middle of its collider, the line
        m_spawnLineBegin = transform.position + ext - Vector3.up;
        //The end is below the beginning
        lineLength = ext.y;
    }

    void Start()
    {
        m_player = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        m_currentDelay += Time.deltaTime;
        if (m_activeObstacles.Count < m_maxObstacles && m_currentDelay > m_delayBetweenSpawn)
        {
            m_currentDelay = 0;
            Vector3 spawnPos = GetNewSpawnPos();
            SpawnableObstacle prefab = GetObjectToSpawn();
            
            m_activeObstacles.Add(SetupObject(prefab, spawnPos));
        }
    }

    private SpawnableObstacle SetupObject(SpawnableObstacle prefab, Vector3 spawnPos)
    {
        SpawnableObstacle tmp = (SpawnableObstacle)m_poolManager.GetObject(prefab.transform, spawnPos, Quaternion.identity);
        tmp.SetVelocity(m_velocity + Random.Range(0, m_randVelocity));
        tmp.m_generator = this;
        tmp.m_player = m_player;
        tmp.m_manager = m_poolManager;
        return tmp;
    }

    //Get random pos alongside the spawn line
    protected Vector3 GetNewSpawnPos()
    {
        Vector3 pos = m_spawnLineBegin;
        pos.y -= UnityEngine.Random.Range(0, lineLength);
        pos.x -= 2;
        return pos;
    }

    protected SpawnableObstacle GetObjectToSpawn()
    {
        int index = UnityEngine.Random.Range(0, m_obstaclePrefabs.Count - 1);
        return m_obstaclePrefabs[index];
    }
    internal void Slowdown()
    {
        for (int i = 0; i < m_activeObstacles.Count; ++i)
        {
            m_activeObstacles[i].Slowdown(m_slowdownVel);
        }
        m_player.Slowdown(m_slowdownVel);
    }

    internal void UnSlowdown()
    {
        for (int i = 0; i < m_activeObstacles.Count; ++i)
        {
            m_activeObstacles[i].UnSlowdown();
        }
        m_player.UnSlowdown();
    }

    public void Pause()
    {
        for (int i = 0; i < m_activeObstacles.Count; ++i)
        {
            m_activeObstacles[i].SetVelocity(0);
        }
    }

    internal void ObjectDestroyed(SpawnableObstacle obj)
    {
        m_activeObstacles.Remove(obj);
    }
}
