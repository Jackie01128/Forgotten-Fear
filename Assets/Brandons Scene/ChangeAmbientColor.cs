using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ChangeAmbientColorBlackAndWhite : MonoBehaviour
{
    public Slider slider;
    public Color blackColor = Color.black;
    public Color whiteColor = Color.white;

    void Update()
    {
        float brightness = slider.GetComponent<Slider>().value;
        RenderSettings.ambientLight = Color.Lerp(blackColor, whiteColor, brightness);
    }
}
