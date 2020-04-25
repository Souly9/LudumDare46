using UnityEngine;
using System.Collections;

public class OxygenPickup : SpawnableObstacle
{
    public float m_storedOxygen;

    void Update()
    {
        CheckPos();
    }

    public override void Init()
    {
        transform.rotation *= Quaternion.Euler(0, 0, Random.Range(-70, 180));
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            OxygenManager.IncreaseOxygen(m_storedOxygen);
            m_audioManager.OxygenSound();
            DestroyMyself();
        }
    }
}
