using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class OxygenManager
{
    private static float m_oxygen;

    public static GameCore m_core;

    public static void InitOxygen(float oxygen)
    {
        m_oxygen = oxygen;
    }

    public static void IncreaseOxygen(float incr)
    {
        if (m_oxygen + incr <= 100)
            m_oxygen += incr;
        else
            m_oxygen = 100.0f;
    }

    public static void DecreaseOxygen(float decr)
    {
        if (decr < m_oxygen)
        {
            m_oxygen -= decr * (float)Math.Pow(1.025, (100.0f - ShipStats.statuses[0])) / (float)Math.Pow(1.05, ShipStats.GetLevel(4));
            ShipStats.SetBarValue(4, m_oxygen);
        }
        else
        {
            ShipStats.SetBarValue(4, 0);
            m_core.GameOver();
        }
    }
}

