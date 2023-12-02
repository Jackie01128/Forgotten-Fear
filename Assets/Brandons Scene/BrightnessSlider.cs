using UnityEngine;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour
{
    public BrightnessController brightnessController;
    public Slider slider;

    public void ChangeBrightness()
    {
        float brightnessValue = slider.value;
        brightnessController.AdjustBrightness(brightnessValue);
    }
}
