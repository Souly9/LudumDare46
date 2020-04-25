using UnityEngine;
using System.Collections;

public class ScrapMetal : SpawnableObstacle
{
    public int m_worth;

    void Awake()
    {
        base.Awake();
    }

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
            m_player.CollectedScrap(m_worth);
            m_audioManager.ScrapSound();
            DestroyMyself();
        }
    }
}
