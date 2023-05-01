using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Deedle;
using Deedle.Internal;
using Unity.VisualScripting;


namespace _Scripts.DataLoader
{
    public enum eyeMovementTypeEnum
    {
        fixation,
        saccade,
        eyesNotFound,
        movementUnclassified
    }

    public class GazeData
    {
        public int? x { get; set; }

        public int? y { get; set; }

        public eyeMovementTypeEnum type { get; set; }

        public float seconds { get; set; }

        public float? AOIHit { get; set; }

        public int fixationSize { get; set; }

        public int speed { get; set; }
    }

    public class ParticipantDataset
    {
        private Frame<int, string> _MetaDF;
        public Frame<int, string> MetaDF;
        public Series<int, double> ParticipantIDS;


        private string _dirPath;


        public ParticipantDataset(string dirPath = "./Assets/Data")
        {
            this._dirPath = dirPath;
            string metaPath = Path.Combine(this._dirPath, "meta.csv");

            this._MetaDF = Frame.ReadCsv(metaPath); //orig
            this.MetaDF = this._MetaDF.Clone(); // filters

            this.ParticipantIDS = this._MetaDF["participantID"];
        }

        public void FilterParticipants(Dictionary<string, int> filterDict)
        {
            string[] allowedFilterKeys = { "drivingLicense", "age", "gender", "navigationType" };

            var metaDF = this._MetaDF.Clone();

            // Debug.Log("Columns");
            // Debug.Log(MetaDF.Columns);

            foreach (var key in allowedFilterKeys)
            {
                foreach (var kvpFilters in filterDict)
                {
                    if (key == kvpFilters.Key.ToString())
                    {
                        metaDF = metaDF.Where(
                            kvp => kvp.Value.GetAs<int>(key) == kvpFilters.Value);
                    }
                    else
                    {
                        // Debug.Log("Filter set up");
                    }
                }
            }

            this.MetaDF = metaDF;
            this.ParticipantIDS = this.MetaDF["participantID"];

            // Debug.Log("DS size");
            // Debug.Log(this.MetaDF.RowCount);
        }

        public void ResetFilters()
        {
            this.MetaDF = this._MetaDF.Clone();
        }


        // Piecharts

        public IEnumerable<KeyValuePair<int, int>> GetGender()
        {
            var gender = this._MetaDF["gender"];
            // Debug.Log(this.df.ColumnKeys);
            var groupedByTags = this._MetaDF["gender"]
                .GroupBy((kvp => (int)(kvp.Value)))
                .Select(kvp => kvp.Value.KeyCount);
            // KeyValuePair<string, OptionalValue<int>>
            var genderDict = groupedByTags.Observations;
            return genderDict;
        }

        public IEnumerable<KeyValuePair<int, int>> GetAge()
        {
            // Debug.Log(this.df.ColumnKeys);
            var groupedByTags = this._MetaDF["age"]
                .GroupBy((kvp => (int)(kvp.Value)))
                .Select(kvp => kvp.Value.KeyCount);
            // KeyValuePair<string, OptionalValue<int>>
            var ageDict = groupedByTags.Observations;
            return ageDict;
        }

        public IEnumerable<KeyValuePair<int, int>> GetDrivingLicence()
        {
            // Debug.Log(this.df.ColumnKeys);
            var groupedByTags = this._MetaDF["drivingLicense"]
                .GroupBy((kvp => (int)(kvp.Value)))
                .Select(kvp => kvp.Value.KeyCount);
            // KeyValuePair<string, OptionalValue<int>>
            var dlDict = groupedByTags.Observations;
            return dlDict;
        }

        public IEnumerable<KeyValuePair<double, int>> GetNavigationType()
        {
            // Debug.Log(this.df.ColumnKeys);
            var groupedByTags = this._MetaDF["navigationType"]
                .GroupBy((kvp => (kvp.Value)))
                .Select(kvp => kvp.Value.KeyCount);
            // KeyValuePair<string, OptionalValue<int>>
            var dlDict = groupedByTags.Observations;
            return dlDict;
        }

        // Others

        public IEnumerable<KeyValuePair<string, string>> GetParticipantDetail(int participant_id)
        {
            string[] detailSelection = { "avgSpeed", "fixationCnt", "saccadeCnt" };
            var row = this.MetaDF.GetRowAt<string>(participant_id);
            var selectedRow = row[detailSelection];
            return selectedRow.Observations;
        }

        public float GetParticipantDuration(int participant_id)
        {
            // returns 
            // id, gameDuration, drivingLicence
            string[] attributeSelection = { "duration", "drivingLicense" };
            var row = this.MetaDF.GetRowAt<string>(participant_id);
            var selectedRow = row["duration"];
            return float.Parse(selectedRow);
        }

        public Dictionary<string, List<int>> GetParticipantsByAge()
        {
            var age_col = this.MetaDF.Columns["age"];
            var min_age = (int)age_col.Min();
            var max_age = (int)age_col.Max();

            var dict = new Dictionary<string, List<int>>();

            foreach (var age in Enumerable.Range(min_age, max_age - min_age + 1))
            {
                var participants = (from kvp in age_col
                    where kvp.Value.ToString() == age.ToString()
                    select kvp.Key);

                dict.Add(age.ToString(), participants.Values.ToList());
            }

            return dict;
        }

        public IEnumerable<KeyValuePair<int, double>> GetParticipantAges()
        {
            var age_id = this.MetaDF["age"];
            // KeyValuePair<string, OptionalValue<int>>
            var ageIdDict = age_id.Observations;
            return ageIdDict;
        }

        public Dictionary<int, double> GetParticipantNavigations()
        {
            var nav_id = this.MetaDF["navigationType"];
            // KeyValuePair<string, OptionalValue<int>>
            var navIdDict = nav_id.Observations;

            var dict = new Dictionary<int, double>();

            foreach (var kv in navIdDict)
            {
                dict.Add(kv.Key, kv.Value);
            }

            return dict;
        }


        public Frame<int, string> GetAOIHit(int participant_id)
        {
            string id_string = participant_id.ToString().PadLeft(3, '0');
            string aoiPath = Path.Combine(this._dirPath, "AOIHit" + id_string + ".csv");

            var aoiHitDF = Frame.ReadCsv(aoiPath);
            return aoiHitDF;
        }

        public double GetVideoOffset(int participatn_id)
        {
            var offsets = this.MetaDF["videoOffset"];
            double offset = offsets.GetAt(participatn_id);
            return offset;
        }

        public List<GazeData> GetParticipantGaze(int participant_id)
        {
            string id_string = participant_id.ToString().PadLeft(3, '0');
            string particPath = Path.Combine(this._dirPath, "Participant" + id_string + ".csv");

            var partGaze = Frame.ReadCsv(particPath);
            var rowCnt = partGaze.RowCount;
            
            var types = partGaze.GetColumn<int>("eyeMovementType");
            var xs = partGaze.GetColumn<int>("gazePointX");
            var ys = partGaze.GetColumn<int>("gazePointY");
            var seconds = partGaze.GetColumn<float>("timeElapsed");
            var AOIHits = partGaze.GetColumn<int>("AOIHit");
            var fixationSize = partGaze.GetColumn<int>("fixationSize");
            var speed = partGaze.GetColumn<int>("speed");


            var GazeList = new List<GazeData>();
            

            foreach (var i in Enumerable.Range(0, rowCnt))
            {
                var gaze = new GazeData()
                {
                    type = (eyeMovementTypeEnum)types.GetAt(i),
                    x = xs.GetAt(i),
                    y = ys.GetAt(i),
                    seconds = seconds.GetAt(i),
                    AOIHit = AOIHits.GetAt(i),
                    fixationSize = fixationSize.GetAt(i),
                    speed = speed.GetAt(i),
                };
                GazeList.Add(gaze);

            }

            return GazeList;
        }
    }
}