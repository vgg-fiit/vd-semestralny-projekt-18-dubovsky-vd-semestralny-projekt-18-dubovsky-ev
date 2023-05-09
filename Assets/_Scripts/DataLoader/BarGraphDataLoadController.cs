using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using System.Text.Json;
//using System.Text.Json.Serialization;
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
    BarGraphGenerator barGraphGenerator;


    void Awake()
    {
        barGraphGenerator = this.GetComponent<BarGraphGenerator>();
    }

    private void Update()
    {
    }

    public void Press(List<BarGraphDataSet> newExampleDataSet)
    {
        barGraphGenerator = this.GetComponent<BarGraphGenerator>();
        barGraphGenerator.GeneratBarGraph(newExampleDataSet);
    }
}