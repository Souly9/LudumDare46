using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//Skeletal class if we decide to add a weapon
public class PlayerAttackController : MonoBehaviour
{
    public float m_weaponDelay;

    public Transform m_barrel;

    public Transform m_bulletPrefab;

    public PoolManager m_poolManager;

    private List<Bullet> m_activeBullets;

    public AudioSource m_fireSound;

    void Start()
    {
        m_activeBullets = new List<Bullet>();
        m_poolManager = FindObjectOfType<PoolManager>();
    }

    public void Shoot()
    {
        m_fireSound.Play();
        Bullet tmp = (Bullet) m_poolManager.GetObject(m_bulletPrefab, m_barrel.position, Quaternion.identity);
        tmp.NormalSpeed();
        tmp.m_controller = this;
        m_activeBullets.Add(tmp);
    }

    public void Pop(Bullet bul)
    {
        m_activeBullets.Remove(bul);
    }

    internal void Slowdown(float vel)
    {
        for (int i = 0; i < m_activeBullets.Count; ++i)
        {
            m_activeBullets[i].Slowdown(vel);
        }
    }

    internal void UnSlowdown()
    {
        for (int i = 0; i < m_activeBullets.Count; ++i)
        {
            m_activeBullets[i].NormalSpeed();
        }
    }
}
