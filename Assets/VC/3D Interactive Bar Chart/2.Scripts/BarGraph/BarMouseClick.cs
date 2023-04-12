using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

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
            int indexBar = (bar.transform.GetSiblingIndex() + 1)/ 2;

            uiInfo.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = "Index: " + (bar.transform.GetSiblingIndex()+1)/2 + ", value: " + bar.transform.GetComponent<BarProperty>().BarLabel.text + "\n" +
                "row: " + bar.transform.parent.name;
            
            //asi by malo ist
            var target = StaticFiltrationController.newBarGraphExampleDataSet[index].ListOfBars[indexBar];
            StaticFiltrationController.targetToShow = target;

            outline.enabled = true;
            PointerDownOnBar(bar);


        }
        public void OnMouseUp()
        {
            transform.localScale = barScale;
            outline.enabled = false;
            PointerUpOnBar(bar);
        }
        public void OnMouseEnter()
        {
            uiInfoToolTip.transform.position = this.transform.position + new Vector3(0, ((bar.transform.localScale.y) / 2) + 2, -1);
            Debug.Log(bar.transform.name);
            uiInfoToolTip.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = bar.transform.name + "\n with value:" + bar.transform.GetComponent<BarProperty>().BarLabel.text;
            uiInfoToolTip.SetActive(true);

            transform.localScale = transform.localScale + new Vector3(0.15f, 0, 0.15f);
            PointerEnterOnBar(bar);
            // outline.enabled = true;

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
