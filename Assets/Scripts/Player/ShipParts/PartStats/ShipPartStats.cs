using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ShipPartStats")]
public class ShipPartStats : ScriptableObject
{
    [Range(0, 100)]
    public float m_lostOnDestruction = 3;
}
