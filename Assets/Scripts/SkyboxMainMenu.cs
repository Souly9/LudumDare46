using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxMainMenu : MonoBehaviour
{
    private float timePassed = 0.0f;
    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        RenderSettings.skybox.SetFloat("_Rotation", timePassed);
    }
}
