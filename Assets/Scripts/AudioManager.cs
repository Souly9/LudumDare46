using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource m_asteroidExplosion;

    public AudioSource m_scrapCollected;

    public AudioSource m_oxygenCollected;

    public void AsteroidSound()
    {
       m_asteroidExplosion.PlayOneShot(m_asteroidExplosion.clip, Random.Range(60, 100));
    }

    public void ScrapSound()
    {
        m_scrapCollected.PlayOneShot(m_scrapCollected.clip, Random.Range(60, 100));
    }
    public void OxygenSound()
    {
        m_oxygenCollected.PlayOneShot(m_oxygenCollected.clip, Random.Range(60, 100));
    }
}