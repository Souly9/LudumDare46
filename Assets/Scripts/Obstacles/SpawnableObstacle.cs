using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public abstract class SpawnableObstacle : MonoBehaviour, IShaker, IPoolable
{
    private float m_velocity;

    public Rigidbody m_myRigid;

    public PlayerManager m_player;

    public PoolManager m_manager;

    // determines if object has passed the player, has to be tinier than 0 since player is at the origin
    [Range(-30, -10)]
    public float m_outOfSightlimit = -20;

    public ObstacleSpawner m_generator;

    // camera Shaker trauma
    public float m_shakeValue = 0;
    // damage this object does to whatever ship part it hits
    [Range(1, 100)]
    public float m_damageValue = 10;

    //pooling variables
    public ObjectPool m_pool { get; set; }
    public GameObject m_gameObject => gameObject;
    public Transform m_transform => transform;

    public AudioManager m_audioManager;

    public virtual void Awake()
    {
        m_audioManager = FindObjectOfType<AudioManager>();
        m_myRigid = GetComponent<Rigidbody>();
        transform.GetComponent<Collider>().isTrigger = true;
    }

    public virtual void SetVelocity(float vel)
    {
        m_velocity = vel;
        m_myRigid.velocity = new Vector3(-m_velocity, 0, 0);
    }

    protected virtual void DestroyMyself()
    {
        m_pool.Return(this);
        m_generator.ObjectDestroyed(this);
    }

    public virtual void Init()
    {
    }

    // checks if the object has passed the player
    protected void CheckPos()
    {
        if(transform.position.x < m_outOfSightlimit) 
            DestroyMyself();
    }

    internal void UnSlowdown()
    {
        SetVelocity(m_velocity);
    }

    internal void Slowdown(float m_slowdownVel)
    {
        m_myRigid.velocity = new Vector3(-m_slowdownVel, 0, 0);
    }
}
