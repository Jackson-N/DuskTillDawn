/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class VignetteController : MonoBehaviour
{
    SanityController sanity;

    public Volume globalVolume;
    private Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        sanity = GetComponent<SanityController>();
        globalVolume.profile.TryGet(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        if(sanity.currentSanity < 100.0f)
        {
            vignette.intensity.value = 0.1f;
        }
        else if(sanity.currentSanity < 75.0f)
        {
            vignette.intensity.value = 0.2f;
        }
        else if(sanity.currentSanity < 50.0f)
        {
            vignette.intensity.value = 0.3f;
        }
        else if(sanity.currentSanity < 25.0f)
        {
            vignette.intensity.value = 0.4f;
        }
        else if(sanity.currentSanity < 10.0f)
        {
            vignette.intensity.value = 0.8f;
        }
        else if(sanity.currentSanity < 0.0f)
        {
            vignette.intensity.value = 1.0f;
        }
    }
}
*/