using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class CameraShaker : MonoBehaviour
{
    //trauma variables
    public float m_maxTrauma;

    private float m_seed;

    public float m_maxAngle, m_maxOffset;
    private float m_offsetX, m_offsetY, m_angleX, m_angleZ;

    private Vector3 m_originalPos;

    void Awake()
    {
        CameraTraumaValue.m_maxTrauma = m_maxTrauma; 
        m_seed = Random.value;
        m_originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraTraumaValue.m_cameraTrauma > 0)
        {
            CalculateTrauma(CameraTraumaValue.m_cameraTrauma);
            CameraTraumaValue.DecreaseShake(Time.deltaTime);
        }
    }

    private void CalculateTrauma(float trauma)
    {
        float finalTrauma = trauma * trauma;

        m_angleX = m_maxAngle * trauma * Mathf.PerlinNoise(m_seed, Time.time);
        m_angleZ = m_maxAngle * trauma * Mathf.PerlinNoise(m_seed + 1, Time.time);

        m_offsetX = m_maxOffset * trauma * Mathf.PerlinNoise(m_seed + 2, Time.time);
        m_offsetY = m_maxOffset * trauma * Mathf.PerlinNoise(m_seed + 3, Time.time);

        transform.position = m_originalPos + new Vector3(m_offsetX, m_offsetY, 0);

        transform.rotation = Quaternion.Euler(m_angleX, 0, m_angleZ);
    }
}
