using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class CameraTraumaValue
{
    public static float m_cameraTrauma { get; set; }

    public static float m_maxTrauma;

    //Method primarily for easier readable code throughout the codebase
    public static void ShakeEvent(float shakeFactor)
    {
        if (m_cameraTrauma >= m_maxTrauma)
            return;
        m_cameraTrauma += shakeFactor;
    }

    public static void DecreaseShake(float val)
    {
        if (m_cameraTrauma <= 0)
        {
            m_cameraTrauma = 0;
            return;
        }
        m_cameraTrauma -= val;
    }
}

