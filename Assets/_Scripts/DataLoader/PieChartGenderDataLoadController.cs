using System.Collections;
using System.Collections.Generic;
using _Scripts.DataLoader;
using UnityEngine;
using PieChart.ViitorCloud;
using static _Scripts.DataLoader.ParticipantDataset;


public class PieChartGenderDataLoadController : MonoBehaviour
{
    [HideInInspector] public List<float> Data = new List<float>();
    [HideInInspector] public List<string> dataDescription = new List<string>();
    [HideInInspector] public List<Color> customColors = new List<Color>();
    [HideInInspector] public int segment;

    public ParticipantDataset participantDataset =
        new ParticipantDataset();


    // Start is called before the first frame update
    void Awake()
    {
        //data
        Data.Add(53.0f);
        dataDescription.Add("Male");
        Data.Add(47.0f);
        dataDescription.Add("Female");

        //Color
        customColors.Add(Color.blue);
        customColors.Add(Color.red);

        //pocet kolko segmentob
        segment = Data.Count;
    }

    // Update is called once per frame
    void Update()
    {
    }
}