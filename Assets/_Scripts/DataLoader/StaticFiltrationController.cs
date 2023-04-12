using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BarGraph.VittorCloud;

public static class StaticFiltrationController
{
    public static string gender = "all";
    public static string navType = "all";
    public static string age = "all";
    public static string licence = "all";

    public static List<BarGraphDataSet> newBarGraphExampleDataSet = new List<BarGraphDataSet>();
    public static List<BarGraphDataSet> newScatterPlotExampleDataSet = new List<BarGraphDataSet>();

    // toto musi byt vlastneho typu nejake struct alebo class - proste s informaciami o tom pozuivatelovi
    public static XYBarValues targetToShow;


    public static void ChangeFiltration(string variable)
    {
        switch (variable)
        {
            case "Male":
                if (gender == "male")
                {
                    gender = "all";
                }
                else
                {
                    gender = "male";
                }
                break;
            case "Female":
                if (gender == "female")
                {
                    gender = "all";
                }
                else
                {
                    gender = "female";
                }
                break;
            case "10-20":
                if (gender == "10-20")
                {
                    age = "all";
                }
                else
                {
                    age = "10-20";
                }
                break;
            case "20-30":
                if (gender == "20-30")
                {
                    age = "all";
                }
                else
                {
                    age = "20-30";
                }
                break;
            case "30-40":
                if (gender == "30-40")
                {
                    age = "all";
                }
                else
                {
                    age = "30-40";
                }
                break;
            case "40-50":
                if (gender == "40-50")
                {
                    age = "all";
                }
                else
                {
                    age = "40-50";
                }
                break;
            case "True":
                if (licence == "true")
                {
                    licence = "all";
                }
                else
                {
                    licence = "true";
                }
                break;
            case "False":
                if (licence == "false")
                {
                    licence = "all";
                }
                else
                {
                    licence = "false";
                }
                break;
            case "Colorful":
                if (navType == "colorful")
                {
                    navType = "all";
                }
                else
                {
                    navType = "colorful";
                }
                break;
            case "Arrows":
                if (navType == "arrows")
                {
                    navType = "all";
                }
                else
                {
                    navType = "arrows";
                }
                break;
            default:
                // code block
                break;
        }
    }
}
