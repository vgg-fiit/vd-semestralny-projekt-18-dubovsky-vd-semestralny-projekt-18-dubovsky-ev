using System.Collections;
using System.Collections.Generic;
using System.IO;
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


        public ParticipantDataset(string dirPath)
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
            var row = this._MetaDF.GetRowAt<string>(participant_id + 1);
            var selectedRow = row[detailSelection];
            return selectedRow.Observations;
        }

        public void GetAOIMatrix()
        {
            // TODO aggregate by 30 seconds from gameStart   
        }

        public IEnumerable<KeyValuePair<string, string>> GetParticipantDuration(int participant_id)
        {
            // returns 
            // id, gameDuration, drivingLicence
            string[] attributeSelection = { "duration", "drivingLicense" };
            var row = this._MetaDF.GetRowAt<string>(participant_id + 1);
            var selectedRow = row[attributeSelection];
            return selectedRow.Observations;
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


    public class MyCubeScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var ds = new ParticipantDataset("/home/awesome/STU/VD/VD_dataset/dataset/transformed");
            ds.GetAge();
            ds.GetGender();
            ds.GetDrivingLicence();

            ds.GetParticipantDetail(10);
            ds.GetParticipantDuration(10);

            foreach (KeyValuePair<string, string> kvp in ds.GetParticipantDuration(10))
            {
                Debug.Log(kvp.Key);
                Debug.Log(kvp.Value);
            }
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}