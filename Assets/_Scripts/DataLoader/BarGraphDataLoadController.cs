using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text.Json;
using System.Text.Json.Serialization;
using _Scripts.DataLoader;
using BarGraph.VittorCloud;
using static _Scripts.DataLoader.ParticipantDataset;

//"startTimestamp", "ParticipantID", "eyeTrackingStartTime", "gameStartTime", "route", "routeSteps", "navigationType", "gender", "age", "drivingLicense",  

/* "speed",
  "keyDown",
  "keyUp",
  "keyLeft",
  "keyRight",
  "timestamp",
  {"posXY":  ["x", "y", "z"]},
  "Gaze point X",
  "Gaze point Y",
  "Eye movement type",
  "AOI hit",
  "navigationElement"
     */

/*Pricemz eye movement type je:
{'Unclassified', 'Fixation', 'Saccade', 'EyesNotFound'}*/
public class BarGraphDataLoadController : MonoBehaviour
{
    private List<XYBarValues> ListOfBars;
    private List<BarGraphDataSet> newExampleDataSet = new List<BarGraphDataSet>();
    BarGraphGenerator barGraphGenerator;
    private int time_interval = 30; // seconds

    private ParticipantDataset participantDataset = new ParticipantDataset();

    void Awake()
    {
        Debug.Log("Awake");
        barGraphGenerator = this.GetComponent<BarGraphGenerator>();
        Press();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Press();
        }
    }

    public void Press()
    {
        Debug.Log("Dataset creation started");

        this.transform.GetComponent<BarGraphExample>().exampleDataSet.Clear();

        int participant_cnt = this.participantDataset.ParticipantIDS.KeyCount;
        int max_time = 14;

        int capacity = participant_cnt * max_time;
        this.transform.GetComponent<BarGraphExample>().exampleDataSet.Capacity = capacity;

        Debug.Log("Participant cnt");
        Debug.Log(participant_cnt);

        foreach (var i in Enumerable.Range(0, participant_cnt))
        {
            this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
            this.transform.GetComponent<BarGraphExample>().exampleDataSet[i].GroupName = "Participant" + i.ToString();
            this.transform.GetComponent<BarGraphExample>().exampleDataSet[i].ListOfBars = new List<XYBarValues>();


            var aoihits = this.participantDataset.GetAOIHit(i);


            foreach (var j in Enumerable.Range(0, aoihits.RowCount))
            {
                var row = aoihits.GetRow<string>(j);
                var row_obs = row.Observations;
                var a = row.Values;

                var time = time_interval * j;

                var xy = new XYBarValues
                {
                    XValue = time.ToString(),
                    YValue = int.Parse(row_obs.First().Value),
                };

                this.transform.GetComponent<BarGraphExample>().exampleDataSet[i].ListOfBars.Add(xy);
            }


            if (aoihits.RowCount < max_time)
            {
                foreach (var k in Enumerable.Range(aoihits.RowCount, max_time - aoihits.RowCount))
                {
                    var time = time_interval * k;

                    var xy = new XYBarValues
                    {
                        XValue = time.ToString(),
                        YValue = 0,
                    };

                    this.transform.GetComponent<BarGraphExample>().exampleDataSet[i].ListOfBars.Add(xy);
                }
            }
        }

        // the duplicit
        // barGraphGenerator.GeneratBarGraph(this.transform.GetComponent<BarGraphExample>().exampleDataSet);
    }
}