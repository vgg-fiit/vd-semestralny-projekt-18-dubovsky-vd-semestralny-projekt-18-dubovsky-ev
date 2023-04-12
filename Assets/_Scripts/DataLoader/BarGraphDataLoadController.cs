using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.Json;
using System.Text.Json.Serialization;
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
public class BarGraphDataLoadController : MonoBehaviour
{
    private List<XYBarValues> ListOfBars;
    private List<BarGraphDataSet> newExampleDataSet = new List<BarGraphDataSet>();
    BarGraphGenerator barGraphGenerator;

    // Start is called before the first frame update
    void Awake()
    {
        barGraphGenerator = this.GetComponent<BarGraphGenerator>();
        /* barGraphGenerator = this.GetComponent<BarGraphGenerator>();
         //"startTimestamp", "ParticipantID", "eyeTrackingStartTime", "gameStartTime", "route", "routeSteps", "navigationType", "gender", "age", "drivingLicense",  

         var participant_first = new ParticipantData
         {
             startTimestamp = Time.time,
             participantID = 1,
             eyeTrackingStartTime = Time.time,
             gameStartTime = Time.time,
             route = 1,
             routeSteps = 3,
             navigationType = "ina",
             gender = "man",
             age = 23,
             drivingLicense = true,
         };

         string json = JsonUtility.ToJson(participant_first);



         //myObject = JsonUtility.FromJson<MyClass>(json);


         //var options = new JsonSerializerOptions { WriteIndented = true };
         //string jsonString = JsonSerializer.Serialize(participant_first, options);

         //Debug.Log(json);
         //Debug.Log(participant_first);
         //Debug.Log(participant_first.age);


         //this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].GroupName = "fero";
         var xy1 = new XYBarValues
         {
             XValue = "3",
             YValue = 44,
         };

         var xy2 = new XYBarValues
         {
             XValue = "3",
             YValue = 60,
         };

         var xy3 = new XYBarValues
         {
             XValue = "31",
             YValue = 20,
         };

         //this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(xy1);
         //this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(xy2);
         //this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(xy3);
         this.transform.GetComponent<BarGraphExample>().exampleDataSet.Capacity = 4;

         this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
         this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
         this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());
         this.transform.GetComponent<BarGraphExample>().exampleDataSet.Add(new BarGraphDataSet());

         this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars = new List<XYBarValues>();
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[1].ListOfBars = new List<XYBarValues>();
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[2].ListOfBars = new List<XYBarValues>();
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[3].ListOfBars = new List<XYBarValues>();
         //bud tento sposob
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(new XYBarValues
         {
             XValue = "31",
             YValue = 3,
         });
         // alebo si to mozes vytvorit nanovo - v podstate to iste
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(xy2);
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(xy3);
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[1].ListOfBars.Add(xy3);
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[1].ListOfBars.Add(xy2);
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[1].ListOfBars.Add(xy3);
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[2].ListOfBars.Add(xy3);
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[2].ListOfBars.Add(xy3);
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[2].ListOfBars.Add(xy1);
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[3].ListOfBars.Add(xy3);
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[3].ListOfBars.Add(xy2);
         this.transform.GetComponent<BarGraphExample>().exampleDataSet[3].ListOfBars.Add(xy2);*/
    }

    private void Update()
    {

    }

    public void Press(List<BarGraphDataSet> newExampleDataSet)
    {
        barGraphGenerator = this.GetComponent<BarGraphGenerator>();
        barGraphGenerator.GeneratBarGraph(newExampleDataSet);
    }
/*

    public void PressQ()
    {

        newExampleDataSet.Clear();

        var xy1 = new XYBarValues
        {
            XValue = "5",
            YValue = 1,
        };

        var xy2 = new XYBarValues
        {
            XValue = "2",
            YValue = 3,
        };

        var xy3 = new XYBarValues
        {
            XValue = "1",
            YValue = 1,
        };

        //this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(xy1);
        //this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(xy2);
        //this.transform.GetComponent<BarGraphExample>().exampleDataSet[0].ListOfBars.Add(xy3);
        newExampleDataSet.Capacity = 4;

        newExampleDataSet.Add(new BarGraphDataSet());
        newExampleDataSet.Add(new BarGraphDataSet());
        newExampleDataSet.Add(new BarGraphDataSet());
        newExampleDataSet.Add(new BarGraphDataSet());

        newExampleDataSet[0].ListOfBars = new List<XYBarValues>();
        newExampleDataSet[1].ListOfBars = new List<XYBarValues>();
        newExampleDataSet[2].ListOfBars = new List<XYBarValues>();
        newExampleDataSet[3].ListOfBars = new List<XYBarValues>();
        //bud tento sposob
        newExampleDataSet[0].ListOfBars.Add(new XYBarValues
        {
            XValue = "2",
            YValue = 1,
        });
        // alebo si to mozes vytvorit nanovo - v podstate to iste
        newExampleDataSet[0].ListOfBars.Add(xy2);
        newExampleDataSet[0].ListOfBars.Add(xy3);
        newExampleDataSet[1].ListOfBars.Add(xy3);
        newExampleDataSet[1].ListOfBars.Add(xy2);
        newExampleDataSet[1].ListOfBars.Add(xy3);
        newExampleDataSet[2].ListOfBars.Add(xy3);
        newExampleDataSet[2].ListOfBars.Add(xy3);
        newExampleDataSet[2].ListOfBars.Add(xy1);
        newExampleDataSet[3].ListOfBars.Add(xy3);
        newExampleDataSet[3].ListOfBars.Add(xy2);
        newExampleDataSet[3].ListOfBars.Add(xy2);




        barGraphGenerator.GeneratBarGraph(newExampleDataSet);
    }*/
}
