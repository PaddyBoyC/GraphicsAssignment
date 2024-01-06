using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSolarSystem : MonoBehaviour
{
    SolarSystem solarSystem;
    SolarSystemSceneGraph solarSystemSceneGraph;

    public Material sunMaterial;
    public Material mercuryMaterial;
    public Material venusMaterial;
    public Material earthMaterial;
    public Material moonMaterial;
    public Material marsMaterial;
    public Material jupiterMaterial;
    public Material saturnMaterial;
    public Material uranusMaterial;
    public Material neptuneMaterial;

    public bool useSceneGraph;

    void Start()
    {
        if (useSceneGraph)
        {
            solarSystemSceneGraph = new SolarSystemSceneGraph(new MyVector(0, 0, 0), new MyVector(0, 0, 0), new MyVector(1, 1, 1), sunMaterial, mercuryMaterial, venusMaterial, earthMaterial, moonMaterial, marsMaterial, jupiterMaterial, saturnMaterial, uranusMaterial, neptuneMaterial);
        }
        else
        {
            solarSystem = new SolarSystem(new MyVector(0, 0, 0), new MyVector(0, 0, 0), new MyVector(1, 1, 1), earthMaterial);
        }
    }

    void Update()
    {
        if (useSceneGraph)
        {
            solarSystemSceneGraph.Update();
        }
    }
}
