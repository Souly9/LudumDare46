using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSystems : MonoBehaviour
{
    public Image[] statusBars;
    public Text[] statusNames;
    public int[] statusVersions;
    public Text[] statusUpgradeCost;
    public Text[] statusRepairCost;
    public Text[] statusTexts;
    public Text metalText;
    public Image cooldownStatus;

    public PlayerManager player;

    public void setStatusBar(int ID, float value)
    {
        statusBars[ID].fillAmount = value / 100;
        statusTexts[ID].text = (value).ToString("F2");
    }

    public float GetStatusBar(int ID)
    {
        return statusBars[ID].fillAmount * 100;
    }

    public void upgradeStatus(int ID)
    {
        statusVersions[ID]++;
        statusNames[ID].text = statusName(ID) + statusVersions[ID].ToString() + ":";
    }

    private string statusName(int ID)
    {
        switch (ID)
        {
            case 0:
                return "Hull MK";
            case 1:
                return "Shield MK";
            case 2:
                return "Engine MK";
            case 3:
                return "Thrusters MK";
            case 4:
                return "Oxygen MK";
            case 5:
                return "Railgun MK";
        }
        return "error";
    }

    // Start is called before the first frame update
    void Start()
    {
        // ShipStats.resetCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        ShipStats.countdownCooldown(Time.deltaTime);
        cooldownStatus.fillAmount = 1.0f - ShipStats.cooldown;
        metalText.text = player.m_scrapWorth.ToString();
        // Updating repair prices
        int tempID = 0;
        while (tempID < 6)
        {
            statusRepairCost[tempID].text = ShipStats.repairCost(tempID).ToString();
            tempID++;
        }
        // Updating upgrade prices
        tempID = 0;
        while (tempID < 6)
        {
            statusUpgradeCost[tempID].text = ShipStats.upgradeCost(tempID).ToString();
            tempID++;
        }
    }
}
