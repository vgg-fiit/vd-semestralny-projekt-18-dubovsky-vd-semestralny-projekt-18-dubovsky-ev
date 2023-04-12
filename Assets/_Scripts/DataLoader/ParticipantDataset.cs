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

        public void FilterParticipants(IEnumerable<KeyValuePair<string, int>> filterDict)
        {
            string[] allowedFilterKeys = { "drivingLicence", "age", "gender" };

            foreach (var key in allowedFilterKeys)
            {
                foreach (var kvpFilters in filterDict)
                {
                    if (key == kvpFilters.Key)
                    {
                        this._MetaDF = this._MetaDF.Where(
                            kvp => kvp.Value.GetAs<int>(key) == kvpFilters.Value);
                    }
                }
            }

            this.ParticipantIDS = this._MetaDF["participantID"];
        }

        public void ResetFilters()
        {
            this.MetaDF = this._MetaDF.Clone();
        }

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

        public IEnumerable<KeyValuePair<string, string>> GetParticipantDetail(int participant_id)
        {
            string[] detailSelection = { "avgSpeed", "fixationCnt", "saccadeCnt" };
            var row = this._MetaDF.GetRowAt<string>(participant_id);
            var selectedRow = row[detailSelection];
            return selectedRow.Observations;
        }

        public float GetParticipantDuration(int participant_id)
        {
            // returns 
            // id, gameDuration, drivingLicence
            string[] attributeSelection = { "duration", "drivingLicense" };
            var row = this._MetaDF.GetRowAt<string>(participant_id);
            var selectedRow = row["duration"];
            return float.Parse(selectedRow);
        }

        public Dictionary<string, List<int>> GetParticipantsByAge()
        {
            var age_col = this._MetaDF.Columns["age"];
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

        // public IEnumerable<int> ParticipantSpeed(int participant_id)
        // {
        //     // TODO speed or speed of eyes
        // }

        public Frame<int, string> GetAOIHit(int participant_id)
        {
            string id_string = participant_id.ToString().PadLeft(3, '0');
            string aoiPath = Path.Combine(this._dirPath, "AOIHit" + id_string + ".csv");

            var aoiHitDF = Frame.ReadCsv(aoiPath);
            return aoiHitDF;
        }
    }
}