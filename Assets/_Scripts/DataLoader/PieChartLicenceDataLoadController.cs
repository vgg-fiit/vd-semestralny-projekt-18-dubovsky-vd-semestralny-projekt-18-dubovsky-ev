using System.Collections;
using System.Collections.Generic;
using _Scripts.DataLoader;
using UnityEngine;
using PieChart.ViitorCloud;
using static _Scripts.DataLoader.ParticipantDataset;


public class PieChartLicenceDataLoadController : MonoBehaviour
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
        foreach (var keyValuePair in participantDataset.GetGender())
        {
            Data.Add(keyValuePair.Value);
            if (keyValuePair.Key == 0)
            {
                dataDescription.Add("False");
                customColors.Add(Color.gray);
            }
            else
            {
                dataDescription.Add("True");
                customColors.Add(Color.cyan);
            }
        }

        segment = Data.Count;
    }

    // Update is called once per frame
    void Update()
    {
    }
}