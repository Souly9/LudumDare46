using UnityEngine;
using System.Collections;

public abstract class ShipPart : MonoBehaviour
{
    public PlayerManager m_player;

    public int m_index;

    public bool m_destroyed;

    protected void Start()
    {
        m_player = GetComponentInParent<PlayerManager>();
    }

    // actual damage logic
    public abstract void SetStatusBar(float dmg);

    // mainly a wrapper for SetStatusBar if we ever need to expand it
    public void DamageTaken(float dmg)
    {
        if(!m_destroyed && m_player.m_shieldDestroyed)
            SetStatusBar(dmg);
    }

    public void Reset()
    {
        m_destroyed = true;
    }
}
