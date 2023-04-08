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
    private List<BarGraphDataSet> exampleDataSet;
    BarGraphGenerator barGraphGenerator;

    void Awake()
    {
        Debug.Log("Dataset creation started");

        var pd = new ParticipantDataset("/home/awesome/STU/VD/VD_dataset/dataset/transformed");
        int participant_cnt = pd.ParticipantIDS.KeyCount;
        int max_time = 25;

        int capacity = participant_cnt * max_time; // todo change 
        this.transform.GetComponent<BarGraphExample>().exampleDataSet.Capacity = capacity;

        foreach (var i in Enumerable.Range(0, participant_cnt))
        {
            this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
            this.transform.GetComponent<BarGraphExample>().exampleDataSet[i].ListOfBars = new List<XYBarValues>();
            this.transform.GetComponent<BarGraphExample>().exampleDataSet[i].GroupName = "Participant" + i.ToString();


            var aoihits = pd.GetAOIHit(i);

            // Debug.Log("aoihits.Count()");
            // Debug.Log(aoihits.RowCount);


            foreach (var j in Enumerable.Range(0, aoihits.RowCount))
            {
                var row = aoihits.GetRow<string>(j);
                var row_obs = row.Observations;
                var a = row.Values;

                var time = 10.0 * j;

                var xy = new XYBarValues
                {
                    XValue = time.ToString(),
                    YValue = int.Parse(row_obs.First().Value),
                };

                this.transform.GetComponent<BarGraphExample>().exampleDataSet[i].ListOfBars.Add(xy);
            }

            if (aoihits.RowCount < max_time)
            {
                foreach (var j in Enumerable.Range(max_time, aoihits.RowCount))
                {
                    var time = 10.0 * j;

                    var xy = new XYBarValues
                    {
                        XValue = time.ToString(),
                        YValue = 0,
                    };

                    this.transform.GetComponent<BarGraphExample>().exampleDataSet[i].ListOfBars.Add(xy);
                }
            }
        }

        // var xy1 = new XYBarValues
        // {
        //     XValue = "3",
        //     YValue = 44,
        // };
        //
        // var xy2 = new XYBarValues
        // {
        //     XValue = "3",
        //     YValue = 60,
        // };
        //
        // var xy3 = new XYBarValues
        // {
        //     XValue = "31",
        //     YValue = 20,
        // };
        //
        // //this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(xy1);
        // //this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(xy2);
        // //this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(xy3);
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet.Capacity = 4;
        //
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
        //
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars = new List<XYBarValues>();
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[1].ListOfBars = new List<XYBarValues>();
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[2].ListOfBars = new List<XYBarValues>();
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[3].ListOfBars = new List<XYBarValues>();
        // //bud tento sposob
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(new XYBarValues
        // {
        //     XValue = "31",
        //     YValue = 3,
        // });
        // // alebo si to mozes vytvorit nanovo - v podstate to iste
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(xy2);
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(xy3);
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[1].ListOfBars.Add(xy3);
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[1].ListOfBars.Add(xy2);
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[1].ListOfBars.Add(xy3);
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[2].ListOfBars.Add(xy3);
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[2].ListOfBars.Add(xy3);
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[2].ListOfBars.Add(xy1);
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[3].ListOfBars.Add(xy3);
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[3].ListOfBars.Add(xy2);
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[3].ListOfBars.Add(xy2);
    }
}