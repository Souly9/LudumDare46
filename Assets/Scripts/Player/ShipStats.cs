using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class ShipStats
{
    public static ShipSystems shipSystems;

    public static List<float> statuses;

    public static List<int> upgradeLevels;

    public static float cooldown;

    public static string score = "00:00";

    public static bool SetBarValue(int sysID, float newVal)
    {
        statuses[sysID] = newVal;
        shipSystems.setStatusBar(sysID, statuses[sysID]);
        if (newVal > 0)
            return true;
        return false;
    }

    public static float GetBarValue(int sysID)
    {
        return shipSystems.GetStatusBar(sysID);
    }

    public static bool DecreaseBarValue(int sysID, float damage)
    {
        if (damage > statuses[sysID])
        {
            shipSystems.setStatusBar(sysID, 0);
            return false;
        }

        statuses[sysID] -= damage;
        shipSystems.setStatusBar(sysID, statuses[sysID]);
        return true;
    }

    public static int GetLevel(int sysID)
    {
        return upgradeLevels[sysID];
    }

    public static void UpgradeLevel(int sysID)
    {
        upgradeLevels[sysID]++;
        shipSystems.upgradeStatus(sysID);
    }

    public static int repairCost(int sysID)
    {
        switch (sysID)
        {
            case 0:
                return ((100 - (int)statuses[sysID]) * 2);
            case 4:
                return ((100 - (int)statuses[sysID]) * 3);
        }
        return ((100 - (int)statuses[sysID]) * 1);
    }

    public static int upgradeCost(int sysID)
    {
        switch (sysID)
        {
            case 0:
                return (upgradeLevels[sysID] * 35 + 25);
            case 1:
                return (upgradeLevels[sysID] * 10 + 40);
            case 2:
                return (upgradeLevels[sysID] * 15 + 20);
            case 3:
                return (upgradeLevels[sysID] * 15 + 15);
            case 4:
                return (upgradeLevels[sysID] * 15 + 10);
            case 5:
                return (upgradeLevels[sysID] * 60 + 40);
            default:
                return 0;
        }
    }

    public static void resetCooldown()
    {
        cooldown = 1.0f;
    }

    public static void countdownCooldown(float deltaTime)
    {
        if (cooldown > 0.0f)
        {
            cooldown -= 0.05f * deltaTime * (float)Math.Pow(1.1, GetLevel(5)) * statuses[5] / 100.0f;
        }
        else
        {
            cooldown = 0.0f;
        }
    }
}

