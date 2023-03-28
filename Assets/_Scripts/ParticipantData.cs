using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


//"startTimestamp", "ParticipantID", "eyeTrackingStartTime", "gameStartTime", "route", "routeSteps", "navigationType", "gender", "age", "drivingLicense",  
[Serializable]
public class ParticipantData
{
    public float startTimestamp { get; set; }
    public int participantID { get; set; }
    public float eyeTrackingStartTime { get; set; }
    public float gameStartTime { get; set; }
    public int route { get; set; }
    public int routeSteps { get; set; }
    public string navigationType { get; set; }
    public string gender { get; set; }
    public int age { get; set; }
    public bool drivingLicense { get; set; }
}
