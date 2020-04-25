using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Shield : ShipPart
{
    public float m_regenerationRate;

    public float m_maxValue = 100f;

    private Collider m_myCollider;

    void Start()
    {
        m_myCollider = GetComponent<Collider>();
        m_player = GetComponentInParent<PlayerManager>();
    }
    // Update is called once per frame
    void Update()
    {
        float shield = ShipStats.GetBarValue(1);
        Color temp = GetComponent<SpriteRenderer>().color;
        temp.a = shield / 100.0f;
        GetComponent<SpriteRenderer>().color = temp;
        if (!m_destroyed && shield < m_maxValue)
            ShipStats.SetBarValue(1, shield + m_regenerationRate);
    }

    public override void SetStatusBar(float dmg)
    {
        dmg = 2.0f *  dmg / Mathf.Pow(1.05f, ShipStats.GetLevel(1));
        if (!ShipStats.DecreaseBarValue(m_index, dmg))
            Destroyed();
    }

    private void Destroyed()
    {
        m_destroyed = true;
        m_myCollider.enabled = false;
    }

    public void Reset()
    {
        m_destroyed = false;
        m_myCollider.enabled = true;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Obstacle"))
        {
            SpawnableObstacle obst = col.GetComponent<SpawnableObstacle>();
            SetStatusBar(obst.m_damageValue);
        }
    }
}
