using UnityEngine;
using System.Collections;
using System;

// Central class that could be used to initialize all difficulty relevant stats 
// Maybe even asteroid damage and so on, if balancing is a problem
public class DifficultyManager : MonoBehaviour
{
    public float m_spawnInterval;

    [Tooltip("Base velocity of all objects")]
    public float m_velocity;

    [Tooltip("The upper limit to the random velocity added to every asteroid")]
    public float m_randomVelocity;

    [Range(0, 1)] public float m_tickStep = 0.01f;
    [Range(0, 1)] public float m_randTickStep = 0.01f;
    [Range(0, 0.1f)] public float m_intervalTickStep = 0.01f;

    [Range(0, 1)] public float m_slowdownVel = 0.5f;

    private ObstacleSpawner m_spawner;
    // Use this for initialization
    void Start()
    {
        SetupSpawner();
    }

    private void SetupSpawner()
    {
        m_spawner = FindObjectOfType<ObstacleSpawner>();
        m_spawner.m_velocity = m_velocity;
        m_spawner.m_delayBetweenSpawn = m_spawnInterval;
        m_spawner.m_randVelocity = m_randomVelocity;
        m_spawner.m_slowdownVel = m_slowdownVel;
    }

    //could be used to increase velocity of asteroids the longer the player survives
    public void NewTick()
    {
        m_spawner.m_velocity += m_tickStep;
    }

    public void NewHourTick()
    {
        if (m_spawner.m_delayBetweenSpawn > 0.01)
            m_spawner.m_delayBetweenSpawn -= m_intervalTickStep;
        m_spawner.m_randVelocity += m_randTickStep;
        m_spawner.m_maxObstacles += 4;
    }
}
