using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    // mainly relevant for joysticks if we want to add those
    public float m_positiveDeadzone;
    public float m_negativeDeadzone;

    public PlayerMoveController m_playerMoveController;

    public PlayerAttackController m_attackController;

    public GameCore m_core;

    void Start()
    {
        m_playerMoveController = FindObjectOfType<PlayerMoveController>();
        m_core = FindObjectOfType<GameCore>();
        m_attackController = FindObjectOfType<PlayerAttackController>();
    }

    void FixedUpdate()
    {
        CheckPlayerControls();
    }

    // method to check all inputs relevant for the ship
    void CheckPlayerControls()
    {
        if (Input.GetAxis("Horizontal") > m_positiveDeadzone || Input.GetAxis("Horizontal") < m_negativeDeadzone)
            m_playerMoveController.MoveHorizontal(Input.GetAxis("Horizontal"));
        if (Input.GetAxis("Vertical") > m_positiveDeadzone || Input.GetAxis("Vertical") < m_negativeDeadzone)
            m_playerMoveController.MoveVertical(Input.GetAxis("Vertical"));

        //actually not sure if this is correct for a button atm 
        if (Input.GetAxis("Slowdown") > m_positiveDeadzone)
            m_core.Slowdown(true);
        else
            m_core.Slowdown(false);
        if ((Input.GetAxis("Fire1") > m_positiveDeadzone || Input.GetAxis("Fire2") > m_positiveDeadzone || Input.GetAxis("Fire3") > m_positiveDeadzone) && ShipStats.cooldown == 0)
        {
            m_attackController.Shoot();
            ShipStats.resetCooldown();
        }
        if (Input.GetAxis("Cancel") > m_positiveDeadzone)
        {
            Application.Quit();
        } 
    }
}
