using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemSceneGraph
{
    MyVector Position { get; set; }
    MyVector Rotation { get; set; }
    MyVector Scale { get; set; }

    public SceneGraphNode RootNode { get; private set; }

    List<Planet> planets = new List<Planet>();

    public SolarSystemSceneGraph(MyVector pPosition, MyVector pRotation, MyVector pScale, Material earthMaterial)
    {
        Position = pPosition;
        Rotation = pRotation;
        Scale = pScale;

        MyMatrix translationRoot = MyMatrix.CreateTranslation(Position);

        RootNode = new SceneGraphNode("root", translationRoot);

        //MyMatrix rotationRoot = MyMatrix.CreateRotation(Rotation);

        MyMatrix scaleRoot = MyMatrix.CreateScale(Scale);

        var rootScaleNode = new SceneGraphNode("rootScale", scaleRoot);
        RootNode.AddChild(rootScaleNode);

        MyVector oneVector = new MyVector(1, 1, 1);

        Planet sun = new Planet(new MyVector(0, 0, 0), oneVector, Color.yellow);
        rootScaleNode.AddChild(sun.RootNode);
        planets.Add(sun);

        Planet earth = new Planet(new MyVector(5, 0, 0), oneVector.Multiply(0.25f), Color.blue, earthMaterial);
        rootScaleNode.AddChild(earth.RootNode);
        planets.Add(earth);

        //SceneGraphNode moonNode = BuildPlanet(new MyVector(5.6f, 0, 0), Color.gray, 0.025f);
        //rootScaleNode.AddChild(moonNode);

        //SceneGraphNode satelliteNode = BuildSatellite(new MyVector(5.2f, 0, 0), 0.01f);
        //rootScaleNode.AddChild(satelliteNode);

        RootNode.Draw(MyMatrix.CreateIdentity());
    }
    
    public void Update()
    {
        var earth = planets[1];
        earth.Update();
        RootNode.Draw(MyMatrix.CreateIdentity());
    }

    private SceneGraphNode BuildSatellite(MyVector pOffset, float scale)
    {
        MyMatrix localTransform = MyMatrix.CreateTranslation(pOffset);
        MyMatrix scaleTransform = MyMatrix.CreateScale(new MyVector(scale, scale, scale));
        MyMatrix rotationTransform = MyMatrix.CreateRotationZ(Mathf.PI / 8);
        MyMatrix comboTransform = localTransform.Multiply(rotationTransform);
        comboTransform = comboTransform.Multiply(scaleTransform);

        SceneGraphNode satelliteNode = new SceneGraphNode("satellite", comboTransform);

        GameObject body1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        SceneGraphNode body1Node = new SceneGraphNode("satellite1", 
            localTransform.Multiply(MyMatrix.CreateScale(new MyVector(4f, 2f, 1f))), 
            body1);

        GameObject body2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        SceneGraphNode body2Node = new SceneGraphNode("satellite2",
            localTransform.Multiply(MyMatrix.CreateScale(new MyVector(1f, 1f, 5f))),
            body2);

        satelliteNode.AddChild(body1Node);
        satelliteNode.AddChild(body2Node);

        return satelliteNode;
    }
}

