using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveController : MonoBehaviour
{
    [Range(1, 20)]
    public float m_verticalVelocity = 5;
    public float m_savedVerticalVel;
    [Range(1, 20)]
    public float m_horizontalVelocity = 5;
    public float m_savedHorizontalVel;

    private Rigidbody m_myRigid;

    // Use this for initialization
    void Awake()
    {
        m_myRigid = GetComponent<Rigidbody>();
    }

    public void MoveHorizontal(float dir)
    {
        if (!(m_myRigid.position.x > 9.5f && dir > 0.0f) && !(m_myRigid.position.x < -6.4f && dir < 0.0f))
        {
            float engineModifier = ShipStats.statuses[2] / 100.0f * Mathf.Pow(1.05f, ShipStats.GetLevel(2));
            m_myRigid.MovePosition(m_myRigid.position + new Vector3(dir * Time.deltaTime * m_horizontalVelocity * engineModifier, 0, 0));
        }
    }

    public void MoveVertical(float dir)
    {
        if (!(m_myRigid.position.y > 6.0f && dir > 0.0f) && !(m_myRigid.position.y < -4.3f && dir < 0.0f))
        {
            float thrustersModifier = 0.7f * ShipStats.statuses[3] / 100.0f * Mathf.Pow(1.05f, ShipStats.GetLevel(3));
            m_myRigid.MovePosition(m_myRigid.position + new Vector3(0, dir * Time.deltaTime * m_verticalVelocity * thrustersModifier, 0));
        }
    }
}
