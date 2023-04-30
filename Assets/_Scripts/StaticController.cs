using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticController : MonoBehaviour
{
    public GameObject sphere;
    public Material material;
    public Texture2D cursor;
    // Start is called before the first frame update
    void Awake()
    {
        StaticFiltrationController.sphereMaterial = material;
        StaticFiltrationController.cursor = cursor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
