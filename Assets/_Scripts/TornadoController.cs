using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Scripts.DataLoader;

public class TornadoController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sphere;
    public GameObject sphereDiff;
    public GameObject sphereAOI;
    public GameObject sphereAOINone;
    public GameObject sphereAOINoneDiff;

    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;


    private float x_pos = 17.0f;
    private float before_x = 0;
    private float before_y = 0;


    public Material material;
    public Mesh mesh;
    private int numInstances = 1;
    private int i=0;
    private int inCube = 0;

    private RenderParams rp;
    private List<Matrix4x4> instData = new List<Matrix4x4>();

    private ParticipantDataset participantDataset = new ();

    private List<GazeData> participantData = new List<GazeData>();

    private GameObject _sphere;
    private GameObject _cube;
    private GameObject firstPoint;
    private GameObject lastPoint;

    public float wholeDistance = 0;
    private float firstCircle = 0;
    private float lastCircle = 0;

    private Coroutine mainCoroutine;

    private float targetId;

    private double videoOffset;

    private float timer = 0;

    private bool gotTime = false;

    private double distanceStart = 0;

    private int offsetIndex = 0;

    void Start()
    {

    }


    public void StartVisualisation(){

        _cube = this.transform.parent.gameObject;
        _cube.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<UnityEngine.Video.VideoPlayer>().url = "./Assets/Videos/Participant"+ (StaticFiltrationController.targetToShow + 1) +"-converted.mp4";
        //Debug.Log(_cube.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<UnityEngine.Video.VideoPlayer>().url);


        rp = new RenderParams(material);
        participantData = participantDataset.GetParticipantGaze((int)StaticFiltrationController.targetToShow);
        videoOffset = participantDataset.GetVideoOffset((int)StaticFiltrationController.targetToShow);

        i = 0;
        foreach (var item in participantData)
        {

            if (item.x != null && item.y != null && item.AOIHit != null)
            {
                if (item.AOIHit == 1)
                {
                    _sphere = GameObject.Instantiate(sphereAOI, new Vector3(0, 0, 0), transform.rotation);
                }
                else if (item.AOIHit == 0)
                {
                    if (item.fixationSize != 100)
                    {
                        _sphere = GameObject.Instantiate(sphere, new Vector3(0, 0, 0), transform.rotation);
                    }
                    else
                    {
                        _sphere = GameObject.Instantiate(sphereDiff, new Vector3(0, 0, 0), transform.rotation);
                    }
                }
                else if (item.AOIHit == -1)
                {
                    if (item.fixationSize != 100)
                    {
                        _sphere = GameObject.Instantiate(sphereAOINone, new Vector3(0, 0, 0), transform.rotation);
                    }
                    else
                    {
                        _sphere = GameObject.Instantiate(sphereAOINoneDiff, new Vector3(0, 0, 0), transform.rotation);
                    }
                }
                else if (item.AOIHit == 2)
                {
                    _sphere = GameObject.Instantiate(sphereAOINone, new Vector3(0, 0, 0), transform.rotation);
                }
                _sphere.transform.rotation = Quaternion.Euler(0, 0, 90);
                _sphere.transform.parent = _cube.transform;
                _sphere.transform.localPosition = new Vector3(_cube.transform.position.x - x_pos,  (-1 ) * (((float)item.y / 120) - 4.5f), ((float)item.x / 120) - 8);
                _sphere.transform.localScale = new Vector3((float)item.fixationSize / 30, _sphere.transform.localScale.y, (float)item.fixationSize / 30);
            }

            if (videoOffset <= x_pos && gotTime == false)
            {
                firstCircle = _sphere.transform.localPosition.x;
                distanceStart = x_pos;
                offsetIndex = i;
                gotTime = true;
            }

            if (i == participantData.Count - 1)
            {
                lastCircle = _sphere.transform.localPosition.x;
            }
            x_pos = item.seconds;
            i++;
        }

        _cube.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<UnityEngine.Video.VideoPlayer>().Pause();


        wholeDistance = firstCircle - lastCircle;

        mainCoroutine = StartCoroutine(MoveCube(_cube.transform.GetChild(0).gameObject, participantData.Count, participantData, 0));
    }

    public void VideoClick(float percent)
    {
        StopCoroutine(mainCoroutine);
        mainCoroutine = StartCoroutine(MoveCube(_cube.transform.GetChild(0).gameObject, participantData.Count, participantData, percent));
    }

    IEnumerator MoveCube(GameObject cube, int count, List<GazeData> data, float percent)
    {
        float last = 0;
        float sec = 0;
        cube.transform.GetChild(0).transform.GetChild(0).GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        //Debug.Log(_cube.transform.position.x - (float)distanceStart - (float)((wholeDistance / 100) * (percent * 100)) + "POSITION");
        Debug.Log((percent) + "PERCENT");
        //Debug.Log((float)distanceStart + "VZDIALENOST ?");
        Debug.Log((float)((wholeDistance / 100) * (percent * 100)) + "PRETOCENIE");
        cube.transform.localPosition = new Vector3(_cube.transform.position.x - (float)distanceStart - (float)((wholeDistance / 100) * (percent * 100)), cube.transform.localPosition.y, cube.transform.localPosition.z);
        //Camera.main.transform.position = new Vector3(_cube.transform.localPosition.x - 20, _cube.transform.localPosition.y + 5, _cube.transform.localPosition.z);
        /*foreach (var item in data)
        {
            sec = item.seconds - last;
            //Debug.Log(sec);
            cube.transform.position = new Vector3(cube.transform.position.x - sec, cube.transform.position.y, cube.transform.position.z);
            last = item.seconds;
            yield return new WaitForSeconds(sec);
        }*/
        //Debug.Log((percent) + "PERCENT");
        //Debug.Log((float)((wholeDistance / 100) * (percent * 100)) + "PRETOCENIE");
        //Debug.Log("INDEX" + ((int)(((data.Count - offsetIndex) * percent)) + offsetIndex));
        //Debug.Log("OFFSETINDEX" + offsetIndex);
        //Debug.Log("FULL" + data.Count);
        Debug.Log(percent + " PERCENT " + ((int)(((data.Count - offsetIndex) * percent)) + offsetIndex) + " FROM " + data.Count);

        for (int t = ((int)(((data.Count - offsetIndex) * percent)) + offsetIndex); t<data.Count; t++)
        {

            while (cube.transform.GetChild(0).transform.GetChild(0).GetComponent<UnityEngine.Video.VideoPlayer>().isPaused)
            {
                    yield return null;
            }
            if (t != 0)
            {
                last = data[t - 1].seconds;
            }

            //Debug.Log(((int)(((data.Count - offsetIndex) * percent)) + offsetIndex) + "INDEX " + t + " FROM  " + data.Count);
            sec = data[t].seconds - last;
            //Debug.Log(sec);
            cube.transform.position = new Vector3(cube.transform.position.x - sec, cube.transform.position.y, cube.transform.position.z);
            last = data[t].seconds;
            yield return new WaitForSeconds(sec);
        }
    }


    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < numInstances; ++i)
        //    instData[i] = Matrix4x4.Translate(new Vector3(-4.5f + i, 0.0f, 5.0f));
        //Graphics.RenderMeshInstanced(rp, mesh, 0, instData);

       


        /*float rand_pos_x = Random.Range(-0.2f, 0.2f);
        float rand_pos_y = Random.Range(-0.2f, 0.2f);
        float scale_x = Random.Range(0.5f, 2);
        float scale_y = Random.Range(-0.2f, 0.2f);
        rand_pos_x += before_x;
        rand_pos_y += before_y;
        numInstances += 1;
        //Debug.Log(numInstances);
        if (rand_pos_y < 0)
            rand_pos_y = 5;
        //Debug.Log(instData.Count);
        //instData.Add(Matrix4x4.Translate(new Vector3(rand_pos_x, rand_pos_y, z_pos)));
        //Graphics.RenderMeshInstanced(rp, mesh, 0, instData);
        GameObject _sphere = GameObject.Instantiate(sphere, new Vector3(-75.31f, 1.31f, -76.5f), transform.rotation);
        _sphere.transform.parent = this.transform;
        _sphere.transform.rotation = Quaternion.Euler(0, 0, 0);
        _sphere.transform.position = new Vector3(rand_pos_x, rand_pos_y, z_pos);
        _sphere.transform.localScale = new Vector3(1, 0.02f, 1);
        z_pos -= 0.02f;
        before_x = _sphere.transform.position.x;
        before_y = _sphere.transform.position.y;*/

        /* float rand_pos_x = Random.Range(-0.2f, 0.2f);
         float rand_pos_y = Random.Range(-0.2f, 0.2f);
         rand_pos_x += before_x;
         rand_pos_y += before_y;
         if (rand_pos_y < 0)
             rand_pos_y = 5;
         GameObject _sphere = GameObject.Instantiate(sphere, new Vector3(-74.15f, 4.188789f, -58f), transform.rotation);
         //_sphere.transform.parent = this.transform;
         _sphere.transform.rotation = Quaternion.Euler(0, 0, 90);
         _sphere.transform.position = new Vector3(x_pos, rand_pos_y, rand_pos_x);
         _sphere.transform.localScale = new Vector3(rand_pos_x, 0.1f, rand_pos_x);
         x_pos -= 0.03f;
         before_x = _sphere.transform.position.x;
         before_y = _sphere.transform.position.y;*/
    }
}
