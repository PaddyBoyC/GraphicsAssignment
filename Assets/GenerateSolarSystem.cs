using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSolarSystem : MonoBehaviour
{
    SolarSystem solarSystem;
    SolarSystemSceneGraph solarSystemSceneGraph;

    public Material earthMaterial;
    public Material mercuryMaterial;
    public bool useSceneGraph;

    void Start()
    {
        if (useSceneGraph)
        {
            solarSystemSceneGraph = new SolarSystemSceneGraph(new MyVector(0, 0, 0), new MyVector(0, 0, 0), new MyVector(1, 1, 1), earthMaterial);
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
