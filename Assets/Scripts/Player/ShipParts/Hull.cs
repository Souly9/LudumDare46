using UnityEngine;
using System.Collections;

// not sure how Hull will be implemented
public class Hull : ShipPart
{
    public override void SetStatusBar(float dmg)
    {
        dmg = dmg / Mathf.Pow(1.05f, ShipStats.GetLevel(0));
        if(!ShipStats.DecreaseBarValue(m_index, dmg))
            m_player.Die();
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
