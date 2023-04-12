using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sphere;
    private float z_pos = 17.0f;
    private float before_x = 0;
    private float before_y = 0;


    public Material material;
    public Mesh mesh;
    private int numInstances = 1;

    private RenderParams rp;
    private List<Matrix4x4> instData = new List<Matrix4x4>();


    void Start()
    {
        rp = new RenderParams(material);
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < numInstances; ++i)
        //    instData[i] = Matrix4x4.Translate(new Vector3(-4.5f + i, 0.0f, 5.0f));
        //Graphics.RenderMeshInstanced(rp, mesh, 0, instData);



        float rand_pos_x = Random.Range(-0.2f, 0.2f);
        float rand_pos_y = Random.Range(-0.2f, 0.2f);
        rand_pos_x += before_x;
        rand_pos_y += before_y;
        numInstances += 1;
        //Debug.Log(numInstances);
        if (rand_pos_y < 0)
            rand_pos_y = 5;
        //Debug.Log(instData.Count);
        //instData.Add(Matrix4x4.Translate(new Vector3(rand_pos_x, rand_pos_y, z_pos)));
        //Graphics.RenderMeshInstanced(rp, mesh, 0, instData);
        //GameObject _sphere = GameObject.Instantiate(sphere, new Vector3(0, 0, 0), transform.rotation);
        //_sphere.transform.parent = this.transform;
        //_sphere.transform.rotation = Quaternion.Euler(0, 90, 90);
        //_sphere.transform.position = new Vector3(rand_pos_x, rand_pos_y, z_pos);
        //_sphere.transform.localScale = new Vector3(rand_pos_x, 0.1f, rand_pos_x);
        z_pos -= 0.02f;
        //before_x = _sphere.transform.position.x;
        //before_y = _sphere.transform.position.y;
    }
}
