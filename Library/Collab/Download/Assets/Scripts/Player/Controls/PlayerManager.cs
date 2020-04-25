using UnityEngine;
using System.Collections;

// should be used to mainly communicate with the player at runtime events
public class PlayerManager : MonoBehaviour
{
    public int m_lives;

    public int m_scrapWorth;


    public void CollectedScrap(int worth)
    {
        m_scrapWorth = worth;
    }

    public void Die()
    {

    }
}
