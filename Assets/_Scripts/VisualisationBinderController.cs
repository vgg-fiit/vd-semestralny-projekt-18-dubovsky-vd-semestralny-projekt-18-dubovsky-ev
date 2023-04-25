using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualisationBinderController : MonoBehaviour
{
    private GameObject spatialCube;

    public GameObject cube1;
    public GameObject cube2;

    public GameObject cube3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnTorandoVisualisation(int i)
    {
        if (i == 0)
        {
            spatialCube = cube1;
        }
        else if (i == 1)
        {
            spatialCube = cube2;
        }
        else if (i == 2)
        {
            spatialCube = cube3;
        }

        if (StaticFiltrationController.targetToShow != null)
        {
            //spatialCube.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Participant " + StaticFiltrationController.targetToShow;
            spatialCube.transform.GetChild(0).transform.GetComponent<TornadoController>().StartVisualisation();
            
        }
    }
}