using System.Collections;
using System.Collections.Generic;
using _Scripts.DataLoader;
using UnityEngine;
using PieChart.ViitorCloud;
using static _Scripts.DataLoader.ParticipantDataset;


public class PieChartAgeDataLoadController : MonoBehaviour
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
        foreach (var keyValuePair in participantDataset.GetAge())
        {
            Data.Add(keyValuePair.Value);
            dataDescription.Add(keyValuePair.Key.ToString());
        }

        // todo color 1 barva, co prechazi
        segment = Data.Count;
    }

    // Update is called once per frame
    void Update()
    {
    }
}