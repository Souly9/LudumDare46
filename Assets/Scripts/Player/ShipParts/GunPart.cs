using UnityEngine;
using System.Collections;

public class GunPart : ShipPart
{
    public ShipPartStats m_myStats;

    public override void SetStatusBar(float dmg)
    {
        dmg = dmg / Mathf.Pow(1.05f, ShipStats.GetLevel(0));
        if (ShipStats.DecreaseBarValue(m_index, dmg))
            Destroyed();
    }

    private void Destroyed()
    {
        m_destroyed = true;
        m_player.GunDestroyed(this);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Obstacle"))
        {
            SpawnableObstacle obst = col.GetComponent<SpawnableObstacle>();
            DamageTaken(obst.m_damageValue);
        }
    }
}
