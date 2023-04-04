using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PieChart.ViitorCloud;

public class PieChartGenderDataLoadController : MonoBehaviour
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
        Data.Add(53.0f);
        dataDescription.Add("Male");
        Data.Add(47.0f);
        dataDescription.Add("Female");

        //Color
        customColors.Add(Color.red);
        customColors.Add(Color.blue);

        //pocet kolko segmentob
        segment = Data.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
