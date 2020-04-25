using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIButtonManager : MonoBehaviour
{
    // tried making a generic method for all buttons but it didn't work out
    // public Button[] upgradeButtons;
    // public Button[] repairButtons;
    /*
    // Start is called before the first frame update
    void Start()
    {
        foreach (Button x in upgradeButtons)
        {
            x.onClick.AddListener(tryUpgrade);
        }
    }
    */

    public Button[] upgradeButtons;
    public Button[] repairButtons;

    private bool[] upgradeStatus = new bool[6];
    private bool[] repairStatus = new bool[6];

    public PlayerManager player;

    private Color lightGrey = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);
    private Color lightRed = new Vector4(0.88f, 0.23f, 0.19f, 1.0f);

    private void Start()
    {
        upgradeButtons[0].onClick.AddListener(upgradeHull);
        repairButtons[0].onClick.AddListener(repairHull);
        upgradeButtons[1].onClick.AddListener(upgradeShield);
        repairButtons[1].onClick.AddListener(repairShield);
        upgradeButtons[2].onClick.AddListener(upgradeEngine);
        repairButtons[2].onClick.AddListener(repairEngine);
        upgradeButtons[3].onClick.AddListener(upgradeThrusters);
        repairButtons[3].onClick.AddListener(repairThrusters);
        upgradeButtons[4].onClick.AddListener(upgradeOxygen);
        repairButtons[4].onClick.AddListener(repairOxygen);
        upgradeButtons[5].onClick.AddListener(upgradeRailgun);
        repairButtons[5].onClick.AddListener(repairRailgun);
    }

    // Update is called once per frame
    void Update()
    {

        int tempID = 0;
        while (tempID < 6)
        {
            if (ShipStats.repairCost(tempID) > player.m_scrapWorth)
            {
                repairButtons[tempID].GetComponent<Image>().color = lightRed;
                repairStatus[tempID] = false;
            }
            else
            {
                repairButtons[tempID].GetComponent<Image>().color = lightGrey;
                repairStatus[tempID] = true;
            }
            if (ShipStats.upgradeCost(tempID) > player.m_scrapWorth)
            {
                upgradeButtons[tempID].GetComponent<Image>().color = lightRed;
                upgradeStatus[tempID] = false;
            }
            else
            {
                upgradeButtons[tempID].GetComponent<Image>().color = lightGrey;
                upgradeStatus[tempID] = true;
            }
            tempID++;
        }
    }

    private void tryUpgrade(int sysID)
    {
        if (upgradeStatus[sysID])
        {
            player.CollectedScrap(-ShipStats.upgradeCost(sysID));
            ShipStats.UpgradeLevel(sysID);
        }
    }

    private void tryRepair(int sysID)
    {
        if (repairStatus[sysID])
        {
            player.CollectedScrap(-ShipStats.repairCost(sysID));
            ShipStats.SetBarValue(sysID, 100);
        }
    }

    private void upgradeHull()
    {
        tryUpgrade(0);
    }
    private void repairHull()
    {
        tryRepair(0);
    }


    private void upgradeShield()
    {
        tryUpgrade(1);
    }
    private void repairShield()
    {
        player.ShieldRepaired();
        tryRepair(1);
    }


    private void upgradeEngine()
    {
        tryUpgrade(2);
    }
    private void repairEngine()
    {
        tryRepair(2);
    }


    private void upgradeThrusters()
    {
        tryUpgrade(3);
    }
    private void repairThrusters()
    {
        tryRepair(3);
    }


    private void upgradeOxygen()
    {
        tryUpgrade(4);
    }
    private void repairOxygen()
    {
        tryRepair(4);
    }


    private void upgradeRailgun()
    {
        tryUpgrade(5);
    }
    private void repairRailgun()
    {
        tryRepair(5);
    }
}
