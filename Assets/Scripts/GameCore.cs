using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCore : MonoBehaviour
{
    public CalendarSystem calendar;
    public ShipSystems shipSystems;

    public PlayerManager m_player;

    public bool paused = false;
    public bool m_slowdown;
    private float timeSinceLastTick = 0.0f;

    public List<float> statuses;

    public List<int> upgradeLevels;

    public ObstacleSpawner m_spawner;

    // Start is called before the first frame update
    void Start()
    {
        ShipStats.shipSystems = shipSystems;
        ShipStats.statuses = statuses;
        ShipStats.upgradeLevels = upgradeLevels;
        OxygenManager.InitOxygen(statuses[3]);
        OxygenManager.m_core = this;
        m_spawner = FindObjectOfType<ObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        { 
            m_spawner.Pause();
        }
        else if(m_slowdown)
        {
            m_spawner.Slowdown();
        }
        else
        {
            timeSinceLastTick = timeSinceLastTick + Time.deltaTime;
            float timeNeededToPassTime = 1.0f;
            if (timeSinceLastTick > timeNeededToPassTime)
            {
                OxygenManager.DecreaseOxygen(0.9f);
                calendar.nextMinute();
                timeSinceLastTick = timeSinceLastTick - 1.0f;
            }
        }
    }

    internal void Slowdown(bool val)
    {
        if (val != m_slowdown)
        {
            m_spawner.UnSlowdown();
            m_slowdown = val;
        }
    }

    public void GameOver()
    {
        paused = true;
        SceneManager.LoadScene("GameOver");
        SceneManager.UnloadSceneAsync("MainGame");
    }
}
