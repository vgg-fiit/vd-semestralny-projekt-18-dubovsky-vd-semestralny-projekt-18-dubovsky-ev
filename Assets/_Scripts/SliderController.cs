using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController : MonoBehaviour
{

    public Slider slider;

    public TextMeshProUGUI text;

    public Material materialPlacky;
    public Material materialPlackyAOI;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            Color color = materialPlacky.color;
            color.a = v;
            materialPlacky.color = color;

            Color color2 = materialPlackyAOI.color;
            color2.a = v;
            materialPlackyAOI.color = color2;

            text.text = v.ToString("0.00");
        });
    }

    // Update is called once per frame
    void Awake()
    {
        Color color = materialPlacky.color;

        text.text = color.a.ToString("0.00");
    }
}
