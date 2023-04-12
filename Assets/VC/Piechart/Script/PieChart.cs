using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PieChart.ViitorCloud
{
    public class PieChart : MonoBehaviour
    {


        private PieChartGenderDataLoadController genderDataScript;
        private PieChartAgeDataLoadController ageDataScript;
        private PieChartNavTypeDataLoadController navTypeDataScript;
        private PieChartLicenceDataLoadController licenceDataScript;

        [Tooltip("Object of PieChartMeshController")]
        public PieChartMeshController pieChartMeshController;

        public GameObject pieChartUIInfo;

        [Tooltip("Each of the parts into which the will be divided")]
        public int segments;

        [Tooltip("The data for the pie\n" +
                 "The size of this list must exact the value of Segment.")]
        public List<float> Data = new List<float>();

        [Tooltip("Main Material that the mesh of the pie will use to rander")]
        public Material mainMaterial;

        [Tooltip("The colors that will be applied on the pie\n" +
                 "The size of this list must exact the value of Segment.")]
        public List<Color> customColors = new List<Color>();

        [SerializeField]
        [Tooltip("Pie chart with not information and title")]
        public bool justCreateThePie;

        [Tooltip("The list of description of the pie.")]
        public List<string> dataDescription = new List<string>();

        [Tooltip("Type of animation which will the pie have.")]
        public PieChartMeshController.AnimationType animationType;

        [Tooltip("Assign the parent Object where the Pie will generate")]
        public Transform parentTransform;

        void Start()
        {
            if (this.GetComponent<PieChartGenderDataLoadController>() != null)
            {
                genderDataScript = this.GetComponent<PieChartGenderDataLoadController>();

                Data = genderDataScript.Data;
                dataDescription = genderDataScript.dataDescription;
                customColors = genderDataScript.customColors;
                segments = genderDataScript.segment;
            }

            if (this.GetComponent<PieChartAgeDataLoadController>() != null)
            {
                ageDataScript = this.GetComponent<PieChartAgeDataLoadController>();

                Data = ageDataScript.Data;
                dataDescription = ageDataScript.dataDescription;
                customColors = ageDataScript.customColors;
                segments = ageDataScript.segment;
            }

            if (this.GetComponent<PieChartNavTypeDataLoadController>() != null)
            {
                navTypeDataScript = this.GetComponent<PieChartNavTypeDataLoadController>();

                Data = navTypeDataScript.Data;
                dataDescription = navTypeDataScript.dataDescription;
                customColors = navTypeDataScript.customColors;
                segments = navTypeDataScript.segment;
            }

            if (this.GetComponent<PieChartLicenceDataLoadController>() != null)
            {
                licenceDataScript = this.GetComponent<PieChartLicenceDataLoadController>();

                Data = licenceDataScript.Data;
                dataDescription = licenceDataScript.dataDescription;
                customColors = licenceDataScript.customColors;
                segments = licenceDataScript.segment;
            }


            if (pieChartMeshController == null)
                pieChartMeshController = gameObject.AddComponent<PieChartMeshController>();
            pieChartMeshController.parent = parentTransform.gameObject;
            pieChartMeshController.pieChartUIInfo = pieChartUIInfo.gameObject;
            Debug.Log(pieChartUIInfo.transform.name);


            //pieChartMeshController.onPointerEnter.AddListener(onPointerClick);

            if (pieChartMeshController == null)
            {
                Debug.LogError("Drag The PieChartMeshController to Scene as PieChartMeshController is null.");
                return;
            }
            if (mainMaterial != null)
                pieChartMeshController.SetMatrialOfPie(mainMaterial);

            pieChartMeshController.SetData(Data.ToArray());
            pieChartMeshController.SetColor(customColors.ToArray());
            pieChartMeshController.SetDescription(dataDescription.ToArray());
            pieChartMeshController.GenerateChart(segments ,animationType, justCreateThePie);

        }

        [ContextMenu("Take SS")]
        void TakeSS()
        {
            ScreenCapture.CaptureScreenshot($"{Application.productName} {GetTimeString()}.png");

            string GetTimeString()
            {
                return System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            }
        }



        public void ReverseAnimation(Animation anim, string AnimationName)
        {
            anim[AnimationName].speed = -1;
            anim[AnimationName].time = anim[AnimationName].length;
            anim.CrossFade(AnimationName);
        }
        public void ForwardAnimation(Animation anim, string AnimationName)
        {
            anim[AnimationName].speed = 1;
            anim[AnimationName].time = 0;
            anim.CrossFade(AnimationName);
        }
    }
}