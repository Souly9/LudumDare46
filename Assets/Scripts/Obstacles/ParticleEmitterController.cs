using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEmitterController : MonoBehaviour, IPoolable
{
    //pooling variables
    public ObjectPool m_pool { get; set; }
    public GameObject m_gameObject => gameObject;
    public Transform m_transform => transform;

    private ParticleSystem m_system;

    void Awake()
    {
        m_system = GetComponent<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!m_system.IsAlive()) 
            m_pool.Return(this);
    }

    public void Init()
    {
    }
}
