using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public GameCore core;

    private float timePassed = 0.0f;
    // Update is called once per frame
    void Update()
    {
        if (!core.paused)
        {
            timePassed += Time.deltaTime;
            RenderSettings.skybox.SetFloat("_Rotation", timePassed);
        }
    }
}
