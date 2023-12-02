using UnityEngine;
using UnityEngine.UI;


public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float reflectionIntensityMultiplier;

    public void Update()
    {
        reflectionIntensityMultiplier = slider.value;
    }
}
