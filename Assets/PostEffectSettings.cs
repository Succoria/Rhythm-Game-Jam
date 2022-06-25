using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostEffectSettings : MonoBehaviour
{
    public float beatIntensity = 1;
    public float decay = 4;
    
    public PostProcessVolume Volume;

    public Vector3 SpawnRotation = new Vector3(0, -90, 90);
    
    private BeatManager beatManager;

    private void Start()
    {
        beatManager = FindObjectOfType<BeatManager>();
        beatManager.OnBeat += (long i) =>
        {
            ChromaticAberration ar = Volume.profile.GetSetting<ChromaticAberration>();
            ar.intensity.value = beatIntensity;
        };
    }

    public void Update()
    {
        ChromaticAberration ar = Volume.profile.GetSetting<ChromaticAberration>();
        ar.intensity.value = ar.intensity.value - decay * Time.deltaTime;
    }
}
