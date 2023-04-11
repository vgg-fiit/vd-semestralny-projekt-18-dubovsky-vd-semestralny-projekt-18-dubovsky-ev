using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text.Json;
using System.Text.Json.Serialization;
using _Scripts.DataLoader;
using BarGraph.VittorCloud;

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


public class ScatterPlotDataLoadController : MonoBehaviour
{
    private List<XYBarValues> ListOfBars;
    private List<BarGraphDataSet> exampleDataSet;
    BarGraphGenerator barGraphGenerator;

    private ParticipantDataset participantDataset = new ParticipantDataset();

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Dataset creation started");

        barGraphGenerator = this.GetComponent<BarGraphGenerator>();

        this.transform.GetComponent<BarGraphExample>().exampleDataSet.Clear();

        var pbe = participantDataset.GetParticipantsByAge();

        int age_category_cnt = pbe.Count;
        int max_category_size = 0;


        foreach (var kvp in pbe)
        {
            if (kvp.Value.Count > max_category_size)
            {
                max_category_size = kvp.Value.Count;
            }
        }


        int capacity = age_category_cnt * age_category_cnt;
        this.transform.GetComponent<BarGraphExample>().exampleDataSet.Capacity = capacity;


        foreach (var i in Enumerable.Range(0, max_category_size))
        {
            var age_categories = pbe.Keys;

            this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
            this.transform.GetComponent<BarGraphExample>().exampleDataSet[i].GroupName = "age";
            this.transform.GetComponent<BarGraphExample>().exampleDataSet[i].ListOfBars = new List<XYBarValues>();


            foreach (var age_category in age_categories)
            {
                var part_id = -1;

                try
                {
                    part_id = pbe[age_category][i];
                }
                catch (ArgumentOutOfRangeException e)
                {
                }

                if (part_id == -1)
                {
                    var xy = new XYBarValues
                    {
                        XValue = age_category,
                        YValue = 0,
                    };
                    this.transform.GetComponent<BarGraphExample>().exampleDataSet[i].ListOfBars.Add(xy);
                }
                else
                {
                    int duration = (int)participantDataset.GetParticipantDuration(part_id);
                    Debug.Log(duration);
                    Debug.Log("duration");

                    var xy = new XYBarValues
                    {
                        XValue = age_category,
                        YValue = duration,
                    };
                    this.transform.GetComponent<BarGraphExample>().exampleDataSet[i].ListOfBars.Add(xy);
                }
            }
        }


        // this.transform.GetComponent<BarGraphExample>().exampleDataSet.Capacity = 6;
        //
        // var xy3 = new XYBarValues
        // {
        //     XValue = "three",
        //     YValue = 3,
        // };
        //
        // var xy2 = new XYBarValues
        // {
        //     XValue = "two",
        //     YValue = 2,
        // };
        //
        // var xy22 = new XYBarValues
        // {
        //     XValue = "two",
        //     YValue = 3,
        // };
        //
        //
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
        // // this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
        // // this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
        // //
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars = new List<XYBarValues>();
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[1].ListOfBars = new List<XYBarValues>();
        //
        // //
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(new XYBarValues
        // {
        //     XValue = "25",
        //     YValue = 21,
        // });
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(new XYBarValues
        // {
        //     XValue = "26",
        //     YValue = 25,
        // });
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(new XYBarValues
        // {
        //     XValue = "27",
        //     YValue = 15,
        // });
        //
        // //
        //
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[1].ListOfBars.Add(new XYBarValues
        // {
        //     XValue = "25",
        //     YValue = 16,
        // });
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[1].ListOfBars.Add(new XYBarValues
        // {
        //     XValue = "26",
        //     YValue = -1,
        // });
        // this.transform.GetComponent<BarGraphExample>().exampleDataSet[1].ListOfBars.Add(new XYBarValues
        // {
        //     XValue = "27",
        //     YValue = 20,
        // });
        //
        // barGraphGenerator.GeneratBarGraph(this.transform.GetComponent<BarGraphExample>().exampleDataSet);
    }
}