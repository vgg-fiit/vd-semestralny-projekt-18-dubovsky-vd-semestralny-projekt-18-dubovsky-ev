using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PieChart.ViitorCloud;

public class PieChartNavTypeDataLoadController : MonoBehaviour
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
        Data.Add(50.0f);
        dataDescription.Add("Colorful");
        Data.Add(50.0f);
        dataDescription.Add("Arrows");

        //Color
        customColors.Add(Color.yellow);
        customColors.Add(Color.green);

        //pocet kolko segmentob
        segment = Data.Count;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
