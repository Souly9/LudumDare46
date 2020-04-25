using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Bullet : MonoBehaviour, IPoolable
{
    public float m_velocity;

    public Rigidbody m_myRigid;

    public PlayerAttackController m_controller;

    //pooling variables
    public ObjectPool m_pool { get; set; }
    public GameObject m_gameObject => gameObject;
    public Transform m_transform => transform;

    // Use this for initialization
    void Awake()
    {
        Init();
        m_myRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(m_myRigid.position.x > 20)
        {
            m_controller.Pop(this);
            m_pool.Return(this);
        }
    }
    public void Init()
    {
    }

    public void NormalSpeed()
    {
        m_myRigid.velocity = new Vector3(m_velocity, 0, 0);
    }

    public void Slowdown(float vel)
    {
        m_myRigid.velocity = new Vector3(vel, 0, 0);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.transform.CompareTag("Obstacle"))
        {
            m_controller.Pop(this);
            m_pool.Return(this);
        }
    }
}
