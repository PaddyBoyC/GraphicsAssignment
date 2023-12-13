using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSolarSystem : MonoBehaviour
{
    SolarSystem solarSystem;

    public Material earthMaterial;

    void Start()
    {
        solarSystem = new SolarSystem(new MyVector(0, 0, 0), new MyVector(0, 0, 0), new MyVector(1, 1, 1), earthMaterial);

    }

    void Update()
    {
        
    }
}
