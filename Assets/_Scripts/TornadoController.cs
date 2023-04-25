using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Scripts.DataLoader;

public class TornadoController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sphere;
    public GameObject sphereAOI;
    public GameObject sphereAOINone;

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

    void Start()
    {

    }


    public void StartVisualisation(){

        _cube = this.transform.parent.gameObject;
        _cube.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<UnityEngine.Video.VideoPlayer>().url = "./Assets/Videos/Participant"+ (StaticFiltrationController.targetToShow + 1) +"-converted.mp4";
        //Debug.Log(_cube.transform.GetChild(0).transform.GetChild(0).GetComponent<UnityEngine.Video.VideoPlayer>().url);


        rp = new RenderParams(material);
        participantData = participantDataset.GetParticipantGaze((int)StaticFiltrationController.targetToShow);
        
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
                    _sphere = GameObject.Instantiate(sphere, new Vector3(0, 0, 0), transform.rotation);
                }
                else if (item.AOIHit == -1)
                {
                    _sphere = GameObject.Instantiate(sphereAOINone, new Vector3(0, 0, 0), transform.rotation);
                }
                else if (item.AOIHit == 2)
                {
                    _sphere = GameObject.Instantiate(sphereAOINone, new Vector3(0, 0, 0), transform.rotation);
                }
                _sphere.transform.rotation = Quaternion.Euler(0, 0, 90);
                _sphere.transform.parent = _cube.transform;
                _sphere.transform.localPosition = new Vector3(_cube.transform.position.x - x_pos, ((float)item.y / 120) - 4.5f, ((float)item.x / 120) - 8);
            }
           
            x_pos = item.seconds;
            i++;
        }

        Debug.Log(_cube.transform.GetChild(0).gameObject.name);
        StartCoroutine(MoveCube(_cube.transform.GetChild(0).gameObject, participantData.Count, participantData));
    }

    IEnumerator MoveCube(GameObject cube, int count, List<GazeData> data)
    {
        float last = 0;
        float sec = 0;
        cube.transform.GetChild(0).transform.GetChild(0).GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        cube.transform.localPosition = new Vector3(_cube.transform.position.x, cube.transform.localPosition.y, cube.transform.localPosition.z);
        foreach (var item in data)
        {
            sec = item.seconds - last;
            //Debug.Log(sec);
            cube.transform.position = new Vector3(cube.transform.position.x - sec, cube.transform.position.y, cube.transform.position.z);
            last = item.seconds;
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
