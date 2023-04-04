using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PieChart.ViitorCloud;

public class PieChartLicenceDataLoadController : MonoBehaviour
{
    [HideInInspector]
    public List<float> Data = new List<float>();
    [HideInInspector]
    public List<string> dataDescription = new List<string>();
    [HideInInspector]
    public List<Color> customColors = new List<Color>();
    [HideInInspector]
    public int segment;


    // Start is called before the first frame update
    void Awake()
    {
        //data
        Data.Add(70.0f);
        dataDescription.Add("True");
        Data.Add(20.0f);
        dataDescription.Add("False");

        //Color
        customColors.Add(Color.cyan);
        customColors.Add(Color.gray);

        //pocet kolko segmentob
        segment = Data.Count;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
