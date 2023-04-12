using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BarGraph.VittorCloud;

public class PieChartFiltrationController : MonoBehaviour
{
    private List<BarGraphDataSet> newExampleDataSet = new List<BarGraphDataSet>();

    public GameObject _barGraph;
    public GameObject _scatterPlot;
    public GameObject system;

    private GameObject[] barGraphs;
    private GameObject[] scatterPlots;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PieChartFiltration()
    {
        //ScatterPlot
        //data creation
        ScatterPlotDataCreation();
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
        //sending new dataset to bargraph
        go_sc.GetComponent<ScatterPlotDataLoadController>().Press(newExampleDataSet);




        //BarGraph
        //data creation
        BarGraphDataCreation();
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
        go.GetComponent<BarGraphDataLoadController>().Press(newExampleDataSet);
    }


    private List<BarGraphDataSet> ScatterPlotDataCreation()
    {

        Debug.Log(StaticFiltrationController.gender);
        Debug.Log(StaticFiltrationController.age);
        Debug.Log(StaticFiltrationController.navType);
        Debug.Log(StaticFiltrationController.licence);

        newExampleDataSet.Clear();

        var xy1 = new XYBarValues
        {
            XValue = "10-20",
            YValue = 2,
        };

        var xy2 = new XYBarValues
        {
            XValue = "20-30",
            YValue = 2,
        };

        var xy3 = new XYBarValues
        {
            XValue = "30-40",
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
            XValue = "31",
            YValue = 3,
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

        return newExampleDataSet;
    }


    private List<BarGraphDataSet> BarGraphDataCreation()
    {

        newExampleDataSet.Clear();

        var xy1 = new XYBarValues
        {
            XValue = "P1",
            YValue = 1,
        };

        var xy2 = new XYBarValues
        {
            XValue = "P2",
            YValue = 3,
        };

        var xy3 = new XYBarValues
        {
            XValue = "P3",
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
            XValue = "31",
            YValue = 3,
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

        return newExampleDataSet;
    }
}
