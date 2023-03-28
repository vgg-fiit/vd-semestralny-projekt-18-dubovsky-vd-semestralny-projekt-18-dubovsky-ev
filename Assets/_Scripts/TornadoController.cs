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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rand_pos_x = Random.Range(-0.2f, 0.2f);
        float rand_pos_y = Random.Range(-0.2f, 0.2f);
        rand_pos_x += before_x;
        rand_pos_y += before_y;
        if (rand_pos_y < 0)
            rand_pos_y = 5;
        GameObject _sphere = GameObject.Instantiate(sphere, new Vector3(0,0,0), transform.rotation);
        //_sphere.transform.parent = this.transform;
        _sphere.transform.rotation = Quaternion.Euler(0, 90, 90);
        _sphere.transform.position = new Vector3(rand_pos_x, rand_pos_y, z_pos);
        _sphere.transform.localScale = new Vector3(rand_pos_x, 0.1f, rand_pos_x);
        z_pos -= 0.03f;
        before_x = _sphere.transform.position.x;
        before_y = _sphere.transform.position.y;
    }
}
