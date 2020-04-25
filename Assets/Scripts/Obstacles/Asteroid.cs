using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Asteroid : SpawnableObstacle
{
    private ParticleSystem m_trail;

    public ParticleSystem m_explosion;

    void Awake()
    {
        base.Awake();

        m_trail = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        CheckPos();
    }

    public override void Init()
    {
        transform.DetachChildren();
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-70, 180));
        m_trail.transform.parent = transform;
    }

    protected override void DestroyMyself()
    {
        base.DestroyMyself();
        m_manager.GetObject(m_explosion.transform, transform.position, Quaternion.identity);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            CameraTraumaValue.ShakeEvent(m_shakeValue);
            m_audioManager.AsteroidSound();
            DestroyMyself();
        }
        else if(col.transform.CompareTag("Bullet"))
        {
            m_audioManager.AsteroidSound();
            DestroyMyself();
        }
    }
}
