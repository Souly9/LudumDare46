using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarSystem : MonoBehaviour
{
    public Text calendar;

    private int currentMinute;
    private int currentHour;
    private int currentDay;

    public DifficultyManager m_manager;

    private void Start()
    {
        currentMinute = 0;
        currentHour = 0;
        currentDay = 0;
        updateTime();
    }

    public void updateTime()
    {
        if (currentDay == 0)
        {
            calendar.text = "Time survived:   " + currentHour.ToString("D2") + ":" + currentMinute.ToString("D2");
            ShipStats.score = currentHour.ToString("D2") + ":" + currentMinute.ToString("D2");
        }
        else if (currentDay == 1)
        {
            calendar.text = "Time survived:   " + "1 Day, " + currentHour.ToString("D2") + ":" + currentMinute.ToString("D2");
            ShipStats.score = "1 Day, " + currentHour.ToString("D2") + ":" + currentMinute.ToString("D2");
        }
        else
        {
            calendar.text = "Time survived:   " + currentDay.ToString() + " Days, " + currentHour.ToString("D2") + ":" + currentMinute.ToString("D2");
            ShipStats.score = currentDay.ToString() + " Days, " + currentHour.ToString("D2") + ":" + currentMinute.ToString("D2");
        }
    }

    public void nextMinute()
    {
        currentMinute++;
        if (currentMinute > 59)
        {
            currentMinute = 0;
            nextHour();
        }
        updateTime();
        //experimental difficulty manager hook
        m_manager.NewTick();
    }


    private void nextHour()
    {
        currentHour++;
        if (currentHour > 23)
        {
            currentHour = 0;
            nextDay();
        }

        m_manager.NewHourTick();
    }

    private void nextDay()
    {
        currentDay++;
    }
}
