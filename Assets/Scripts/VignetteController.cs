
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class VignetteController : MonoBehaviour
{
    public SanityController sanity;

    public HeatController heat;

    //get ref to tunneling vignette controller script
    private Volume globalVolume;
    private Vignette vignette;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = this.gameObject;
        globalVolume = GetComponent<Volume>();
        if (globalVolume != null)
        {
            globalVolume.profile.TryGet(out vignette);
        }
        else
        {
            Debug.LogError("No volume found on " + target.name);
        }
        if (vignette == null)
        {
            Debug.LogError("No vignette found on " + target.name);
        }
        else
        {
            vignette.intensity.value = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sanity != null)
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
        else return;

        if (heat != null)
        {
            if (heat.warmth > 50.0f)
            {
                vignette.intensity.value = 0.3f;
            }
            else if (heat.warmth > 40.0f)
            {
                vignette.intensity.value = 0.4f;
            }
            else if (heat.warmth > 30.0f)
            {
                vignette.intensity.value = 0.5f;
            }
            else if (heat.warmth > 20.0f)
            {
                vignette.intensity.value = 0.6f;
            }
            else if (heat.warmth > 10.0f)
            {
                vignette.intensity.value = 0.7f;
            }
        }
        else return;
    }
}
