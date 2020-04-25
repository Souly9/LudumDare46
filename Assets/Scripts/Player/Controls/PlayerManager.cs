using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography;

// should be used to mainly communicate with the player at runtime events
public class PlayerManager : MonoBehaviour
{
    public int m_scrapWorth;

    public bool m_shieldDestroyed
    {
        get => m_shield.m_destroyed;
    }

    private Shield m_shield;

    public PlayerAttackController m_attackController;

    public PlayerMoveController m_moveController;

    public GameCore m_core;

    void Start()
    {
        m_attackController = GetComponent<PlayerAttackController>();
        m_moveController = GetComponent<PlayerMoveController>();
        m_core = FindObjectOfType<GameCore>();
        m_moveController.m_savedVerticalVel = m_moveController.m_verticalVelocity;
        m_moveController.m_savedHorizontalVel = m_moveController.m_horizontalVelocity;
        m_shield = GetComponentInChildren<Shield>();
    }

    public void ShieldRepaired()
    {
        m_shield.Reset();
    }

    public void CollectedScrap(int worth)
    {
        m_scrapWorth += worth;
    }

    internal void GunDestroyed(GunPart gunPart)
    {
        m_attackController.m_weaponDelay += gunPart.m_myStats.m_lostOnDestruction;
    }

    internal void EngineDestroyed(EnginePart enginePart)
    {
        m_moveController.m_horizontalVelocity -= enginePart.m_myStats.m_lostOnDestruction;
    }

    public void ThrusterDestroyed(Thruster thruster)
    {
        m_moveController.m_verticalVelocity -= thruster.m_myStats.m_lostOnDestruction;
    }

    public void OxygenDestroyed(OxygenTank oxygen)
    {
        // will have to decrease max Bar
    }

    internal void UnSlowdown()
    {
        m_moveController.m_verticalVelocity = m_moveController.m_savedVerticalVel;
        m_moveController.m_horizontalVelocity = m_moveController.m_savedHorizontalVel;
        m_attackController.UnSlowdown();
    }

    internal void Slowdown(float slowdownVel)
    { 
        m_moveController.m_verticalVelocity = slowdownVel;
        m_moveController.m_horizontalVelocity = slowdownVel; 
        m_attackController.Slowdown(slowdownVel);
    }

    public void Die()
    {
        m_core.GameOver();
    }
}
