using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BrightnessController : MonoBehaviour
{
    public Volume postProcessingVolume;
    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        if (postProcessingVolume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.active = true;
        }
    }

    public void AdjustBrightness(float brightness)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.postExposure.value = brightness;
        }
    }
}
