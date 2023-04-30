using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BarGraph.VittorCloud;

public static class StaticFiltrationController
{
    public static string gender = "all";
    public static string navType = "all";
    public static string age = "all";
    public static string licence = "all";

    public static Material sphereMaterial;

    public static List<BarGraphDataSet> newBarGraphExampleDataSet = new List<BarGraphDataSet>();
    public static List<BarGraphDataSet> newScatterPlotExampleDataSet = new List<BarGraphDataSet>();

    // toto musi byt vlastneho typu nejake struct alebo class - proste s informaciami o tom pozuivatelovi
    public static int? targetToShow;


    public static void ChangeFiltration(string variable)
    {
        string[] gender_lst = { "man", "woman" };
        string[] licence_lst = { "true", "false" };
        string[] navtype_lst = { "rectangles", "arrows" };

        var variable_lower = variable.ToLower();

        if (gender_lst.Contains(variable_lower))
        {
            if (gender == variable_lower)
                gender = "all";
            else
                gender = variable_lower;
        }

        else if (licence_lst.Contains(variable_lower))
        {
            if (licence == variable_lower)
                licence = "all";
            else
                licence = variable_lower;
        }

        else if (navtype_lst.Contains(variable_lower))
        {
            if (navType == variable_lower)
                navType = "all";
            else
            {
                navType = variable_lower;
            }
        }
        else
        {
            if (age == variable_lower)
                age = "all";
            else
            {
                age = variable_lower;
            }
        }
    }
}