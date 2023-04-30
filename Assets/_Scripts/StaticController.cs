using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticController : MonoBehaviour
{
    public GameObject sphere;
    public Material material;
    // Start is called before the first frame update
    void Awake()
    {
        StaticFiltrationController.sphereMaterial = material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
