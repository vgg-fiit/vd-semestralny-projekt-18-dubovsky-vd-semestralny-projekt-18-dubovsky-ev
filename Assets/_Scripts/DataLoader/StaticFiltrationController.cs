using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticFiltrationController
{
    public static string gender = "all";
    public static string navType = "all";
    public static string age = "all";
    public static string licence = "all";


    public static void ChangeFiltration(string variable)
    {
        switch (variable)
        {
            case "Male":
                gender = "male";
                break;
            case "Female":
                gender = "female";
                break;
            case "10-20":
                age = "10-20";
                break;
            case "20-30":
                age = "20-#0";
                break;
            case "30-40":
                age = "30-40";
                break;
            case "40-50":
                age = "40-50";
                break;
            case "True":
                licence = "true";
                break;
            case "False":
                licence = "false";
                break;
            case "Colorful":
                licence = "colorful";
                break;
            case "Arrows":
                licence = "arrows";
                break;
            default:
                // code block
                break;
        }
    }
}
