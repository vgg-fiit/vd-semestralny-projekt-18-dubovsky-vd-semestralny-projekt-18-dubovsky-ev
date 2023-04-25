using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using System.Linq;
using _Scripts.DataLoader;

namespace BarGraph.VittorCloud
{
    public class BarMouseClick : MonoBehaviour
    {
        #region PublicVariables

        public Vector3 barScale;
        public Outline outline;

        public Action<GameObject> PointerDownOnBar;
        public Action<GameObject> PointerUpOnBar;
        public Action<GameObject> PointerEnterOnBar;
        public Action<GameObject> PointerExitOnBar;

        public GameObject UIInfo;

        private GameObject player;

        private GameObject uiInfo;
        private GameObject uiInfoToolTip;

        #endregion

        #region PrivateVariables

        GameObject bar;

        #endregion

        #region UnityCallBacks

        private void Awake()
        {
            bar = transform.parent.gameObject;
            player = GameObject.FindGameObjectsWithTag("MainCamera")[0];
        }

        // Start is called before the first frame update
        void Start()
        {
            barScale = transform.localScale;
            outline.enabled = false;

            GameObject Canvas = GameObject.FindGameObjectWithTag("Canvas");
            uiInfo = Canvas.transform.Find("BarChartClickInfo").gameObject;
            uiInfoToolTip = Canvas.transform.Find("ToolTipBarChartClickInfo").gameObject;
        }


        #region UnityMouseEvents

        public void OnMouseDown()
        {
            transform.localScale = transform.localScale + new Vector3(0.15f, 0, 0.15f);

            //GameObject UI = Instantiate(UIInfo, this.transform.position, Quaternion.identity);
            int index = int.Parse(bar.transform.parent.name);
            int indexBar = (bar.transform.GetSiblingIndex() + 1) / 2;

            //asi by malo ist
            var target_str = StaticFiltrationController.newBarGraphExampleDataSet[index].GroupName;
            target_str = target_str.Remove(0, 11);
            var target_int = int.Parse(target_str);

            uiInfo.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text =
                "Index: " + (bar.transform.GetSiblingIndex() + 1) / 2 + ", value: " +
                bar.transform.GetComponent<BarProperty>().BarLabel.text + "\n" +
                "row: " + bar.transform.parent.name + ", participant: " + target_int;

            StaticFiltrationController.targetToShow = target_int;

            outline.enabled = true;
            PointerDownOnBar(bar);
        }

        public void OnMouseUp()
        {
            transform.localScale = barScale;
            outline.enabled = false;
            PointerUpOnBar(bar);
        }

        // hover
        public void OnMouseEnter()
        {
            Debug.Log("Parent search");
            if (this.transform.parent.parent.parent.parent.parent.name.StartsWith("Scatter"))
            {
                var pariticipant_id = int.Parse(bar.transform.parent.name);

                var pd = new ParticipantDataset();
                var info = pd.GetParticipantDetail(pariticipant_id);

                var text = "";
                string[] new_keys = { "Fixation count", "Saccade Count" };

                foreach (var i in Enumerable.Range(0, 2))
                {
                    text = text + new_keys[i] + ": ";
                    text = text + info.ElementAt(i).Value + "\n";
                }

                uiInfoToolTip.transform.position =
                    this.transform.position + new Vector3(0, ((bar.transform.localScale.y) / 2) + 2, -1);

                uiInfoToolTip.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>()
                    .text = text;

                uiInfoToolTip.SetActive(true);

                transform.localScale = transform.localScale + new Vector3(0.15f, 0, 0.15f);
                PointerEnterOnBar(bar);
            }
        }

        public void OnMouseExit()
        {
            uiInfoToolTip.SetActive(false);
            transform.localScale = barScale;
            outline.enabled = false;
            PointerExitOnBar(bar);
        }

        #endregion

        #endregion
    }
}