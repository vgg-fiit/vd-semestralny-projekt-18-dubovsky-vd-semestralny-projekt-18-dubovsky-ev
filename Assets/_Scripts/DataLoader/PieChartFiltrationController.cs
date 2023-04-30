using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BarGraph.VittorCloud;
using _Scripts.DataLoader;
using Palmmedia.ReportGenerator.Core.Reporting.Builders.Rendering;
using static _Scripts.DataLoader.ParticipantDataset;

public class PieChartFiltrationController : MonoBehaviour
{
    private List<BarGraphDataSet> newExampleDataSet = new List<BarGraphDataSet>();

    public GameObject _barGraph;
    public GameObject _scatterPlot;
    public GameObject system;

    private GameObject[] barGraphs;
    private GameObject[] scatterPlots;

    private ParticipantDataset participantDataset = new();

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private Dictionary<string, int> ConvertFiltrationValues()
    {
        var dict = new Dictionary<string, int>();

        if (StaticFiltrationController.gender.ToLower() == "man")
        {
            dict.Add("gender", 0);
        }
        else if (StaticFiltrationController.gender.ToLower() == "woman")
        {
            dict.Add("gender", 1);
        }

        if (StaticFiltrationController.licence.ToLower() == "true")
        {
            dict.Add("drivingLicence", 1);
        }
        else if (StaticFiltrationController.licence.ToLower() == "false")
        {
            dict.Add("drivingLicence", 0);
        }

        if (StaticFiltrationController.navType.ToLower() == "arrows")
        {
            dict.Add("navigationType", 1);
        }
        else if (StaticFiltrationController.navType.ToLower() == "rectangles")
        {
            dict.Add("navigationType", 0);
        }

        if (StaticFiltrationController.age != "all")
        {
            var age = int.Parse(StaticFiltrationController.age);
            dict.Add("age", age);
        }


        // Debug.Log("DICT");
        // foreach (var kv in dict)
        // {
        //     Debug.Log(kv.Key);
        //     Debug.Log(kv.Value);
        // }

        return dict;
    }

    public void PieChartFiltration()

    {
        var filters = ConvertFiltrationValues();
        this.participantDataset.FilterParticipants(filters);

        //ScatterPlot
        //data creation
        newExampleDataSet = ScatterPlotDataCreation();
        //destroying old bargraphs
        scatterPlots = GameObject.FindGameObjectsWithTag("ScatterPlot");

        foreach (var scatterPlot in scatterPlots)
        {
            Destroy(scatterPlot.gameObject);
        }

        //instantiation of new bargraph
        GameObject go_sc = Instantiate(_scatterPlot, new Vector3(-57.21429f, 0.2f, 14.26064f), Quaternion.identity);
        go_sc.transform.parent = system.transform;
        go_sc.transform.name = "Scatterplot";

        StaticFiltrationController.newScatterPlotExampleDataSet = newExampleDataSet;

        if (newExampleDataSet.Count > 0)
        {
            go_sc.GetComponent<ScatterPlotDataLoadController>().Press(newExampleDataSet);
        }
        else
        {
            // TODO hlaska
            Debug.Log("Can not plot dataset with no data");
        }

        //sending new dataset to bargraph

        //BarGraph
        //data creation
        newExampleDataSet = BarGraphDataCreation();
        //destroying old bargraphs
        barGraphs = GameObject.FindGameObjectsWithTag("BarGraph");

        foreach (var barGraph in barGraphs)
        {
            Destroy(barGraph.gameObject);
        }

        //instantiation of new bargraph
        GameObject go = Instantiate(_barGraph, new Vector3(-35.21429f, 0.2f, 14.26064f), Quaternion.identity);
        go.transform.parent = system.transform;
        go.transform.name = "BarGraph";

        StaticFiltrationController.newBarGraphExampleDataSet = newExampleDataSet;
        //sending new dataset to bargraph
        Debug.Log("Dataset 2 INFO::: Count: " + newExampleDataSet.Count
                                              + "Capacity: " + newExampleDataSet.Capacity
                                              + "Group name:  " + newExampleDataSet[0].GroupName
                                              + "First bar count:  " + newExampleDataSet[0].ListOfBars.Count
        );
        foreach (var barData in newExampleDataSet[0].ListOfBars)
        {
            Debug.Log("Bar data: "
                      + " " + barData.XValue
                      + " " + barData.YValue
                      + " " + barData.navType
            );
        }

        if (newExampleDataSet.Count > 0)
        {
            go.GetComponent<BarGraphDataLoadController>().Press(newExampleDataSet);
        }
        else
        {
            // TODO hlaska
            Debug.Log("Can not plot dataset with no data");
        }


        this.participantDataset.ResetFilters();
    }


    private List<BarGraphDataSet> ScatterPlotDataCreation()
    {
        int participant_cnt = this.participantDataset.ParticipantIDS.KeyCount;
        int time_interval = 30; // seconds

        newExampleDataSet.Clear();

        var pbe = participantDataset.GetParticipantsByAge();
        int age_category_cnt = pbe.Count;
        var age_categories = pbe.Keys.ToList();
        var participant_ages = participantDataset.GetParticipantAges();
        var participant_navigations = participantDataset.GetParticipantNavigations();

        foreach (var par_age in participant_ages)
        {
            var participant_age = (int)par_age.Value;
            var participant_id = par_age.Key;
            var participant_nav = (int)participant_navigations[participant_id];
            bool participant_nav_bool = participant_nav != 0;

            // Debug.Log("Part age");
            // Debug.Log(participant_age);

            newExampleDataSet.Add(new BarGraphDataSet());
            newExampleDataSet.Last().ListOfBars = new List<XYBarValues>();
            newExampleDataSet.Last().GroupName = "Participant" + participant_id.ToString();


            foreach (var age_category in age_categories)
            {
                if (age_category == participant_age.ToString())
                {
                    var xy = new XYBarValues
                    {
                        XValue = age_category,
                        YValue = participant_age,
                        navType = participant_nav_bool,
                    };
                    newExampleDataSet.Last().ListOfBars.Add(xy);
                }
                else
                {
                    var xy = new XYBarValues
                    {
                        XValue = age_category,
                        YValue = -1,
                    };
                    newExampleDataSet.Last().ListOfBars.Add(xy);
                }
            }
        }

        return newExampleDataSet;
    }


    private List<BarGraphDataSet> BarGraphDataCreation()
    {
        int time_interval = 30;
        int participant_cnt = this.participantDataset.ParticipantIDS.KeyCount;
        int max_time = 14;
        int capacity = participant_cnt * max_time;

        newExampleDataSet.Clear();
        newExampleDataSet.Capacity = capacity;
        //
        // Debug.Log("Participant cnt");
        // Debug.Log(participant_cnt);

        foreach (var i in Enumerable.Range(0, participant_cnt))
        {
            newExampleDataSet.Add(new BarGraphDataSet());
            newExampleDataSet[i].GroupName = "Participant" + i.ToString();
            newExampleDataSet[i].ListOfBars = new List<XYBarValues>();

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

                newExampleDataSet[i].ListOfBars.Add(xy);
            }


            if (aoihits.RowCount < max_time)
            {
                foreach (var k in Enumerable.Range(aoihits.RowCount, max_time - aoihits.RowCount))
                {
                    var time = time_interval * k;
                    var xy = new XYBarValues
                    {
                        XValue = time.ToString(),
                        YValue = -1,
                    };

                    newExampleDataSet[i].ListOfBars.Add(xy);
                }
            }
        }

        return newExampleDataSet;
    }
}