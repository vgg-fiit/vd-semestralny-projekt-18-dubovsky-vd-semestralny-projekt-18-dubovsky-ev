using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticController : MonoBehaviour
{
    public GameObject sphere;
    public Material material;
    public Texture2D cursor;

    public GameObject uiHelp;

    public GameObject uiParametrization;

    public Material materialPlacky;
    public Material materialPlackyAOI;
    // Start is called before the first frame update
    void Awake()
    {
        StaticFiltrationController.sphereMaterial = material;
        StaticFiltrationController.cursor = cursor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StaticFiltrationController.cameraStatic = !StaticFiltrationController.cameraStatic;
            Debug.Log(StaticFiltrationController.cameraStatic);
        }

    }

    public void ShowHelp()
    {
        uiHelp.SetActive(true);
    }

    public void CloseHelp()
    {
        uiHelp.SetActive(false);
    }


    public void ShowParametrization()
    {
        uiParametrization.SetActive(true);
    }

    public void CloseParametrization()
    {
        uiParametrization.SetActive(false);
    }
    public void GoToGraph()
    {
        Camera.main.transform.localPosition = new Vector3(-37.43f, 5.9f, -41.96f);
        Camera.main.transform.rotation = Quaternion.Euler(13.787f, -3.243f, -0.774f);
        StaticFiltrationController.cameraStaticSpatialCube = false;
    }

    public void GoToLastCube()
    {

        if (StaticFiltrationController.cameraStaticSpatialCubeLastCube != null)
        {
            StaticFiltrationController.cameraStaticSpatialCube = true;
            Camera.main.transform.localPosition = new Vector3(StaticFiltrationController.cameraStaticSpatialCubeLastCube.transform.GetChild(0).transform.GetChild(0).transform.position.x, 5.9f, -41.96f);
            Camera.main.transform.rotation = Quaternion.Euler(13.787f, -3.243f, -0.774f);
        }
      
    }

    public void SetColorRed()
    {
        Color color = materialPlacky.color;
        color = Color.red;
        materialPlacky.color = color;
    }

    public void SetColorRedAOI()
    {
        Color color = materialPlackyAOI.color;
        color = Color.red;
        materialPlackyAOI.color = color;
    }


    public void SetColorGreen()
    {
        Color color = materialPlacky.color;
        color = Color.green;
        materialPlacky.color = color;
    }

    public void SetColorGreemAOI()
    {
        Color color = materialPlackyAOI.color;
        color = Color.green;
        materialPlackyAOI.color = color;
    }


    public void SetColorBlue()
    {
        Color color = materialPlacky.color;
        color = Color.blue;
        materialPlacky.color = color;
    }

    public void SetColorBlueAOI()
    {
        Color color = materialPlackyAOI.color;
        color = Color.blue;
        materialPlackyAOI.color = color;
    }

    public void SetColorPink()
    {
        Color color = materialPlacky.color;
        color = Color.magenta;
        materialPlacky.color = color;
    }

    public void SetColorPinkAOI()
    {
        Color color = materialPlackyAOI.color;
        color = Color.magenta;
        materialPlackyAOI.color = color;
    }

    public void SetColorYellow()
    {
        Color color = materialPlacky.color;
        color = Color.yellow;
        materialPlacky.color = color;
    }

    public void SetColorYellowAOI()
    {
        Color color = materialPlackyAOI.color;
        color = Color.yellow;
        materialPlackyAOI.color = color;
    }
}
