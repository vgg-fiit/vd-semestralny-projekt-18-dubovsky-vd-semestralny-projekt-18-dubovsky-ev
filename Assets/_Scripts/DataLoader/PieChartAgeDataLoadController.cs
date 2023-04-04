using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PieChart.ViitorCloud;

public class PieChartAgeDataLoadController : MonoBehaviour
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
        Data.Add(10.0f);
        dataDescription.Add("10-20");
        Data.Add(40.0f);
        dataDescription.Add("20-30");
        Data.Add(30.0f);
        dataDescription.Add("30-40");
        Data.Add(20.0f);
        dataDescription.Add("40-50");

        //Color
        // Ked tu nedas definovanu farbu - tak sice hodi error ale da im random farbu
        //customColors.Add(Color.red);
        //customColors.Add(Color.blue);

        //pocet kolko segmentob
        segment = Data.Count;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
