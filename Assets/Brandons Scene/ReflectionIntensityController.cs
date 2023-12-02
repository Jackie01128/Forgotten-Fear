using UnityEngine;
using UnityEngine.UI;

public class ReflectionIntensityController : MonoBehaviour
{
    public Slider BrightnessSlider; // Reference to your slider object
    public Material material; // Reference to your material

    void Update()
    {
        material.SetFloat("m_ReflectionIntensity", BrightnessSlider.value);
    }
}
