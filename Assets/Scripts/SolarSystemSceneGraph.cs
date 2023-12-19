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

        SceneGraphNode sunNode = BuildPlanet(new MyVector(0, 0, 0), Color.yellow, 1);
        rootScaleNode.AddChild(sunNode);

        SceneGraphNode earthNode = BuildPlanet (new MyVector(5, 0 , 0), Color.blue, 0.25f, earthMaterial);
        rootScaleNode.AddChild(earthNode);

        SceneGraphNode moonNode = BuildPlanet(new MyVector(5.6f, 0, 0), Color.gray, 0.025f);
        rootScaleNode.AddChild(moonNode);

        SceneGraphNode satelliteNode = BuildSatellite(new MyVector(5.2f, 0, 0), 0.01f);
        rootScaleNode.AddChild(satelliteNode);

        RootNode.Draw(MyMatrix.CreateIdentity());
    }
    
    private SceneGraphNode BuildPlanet(MyVector pOffset, Color color, float scale, Material material = null)
    { 
        MyMatrix localTransform = MyMatrix.CreateTranslation(pOffset);
        MyMatrix scaleTransform = MyMatrix.CreateScale(new MyVector(scale, scale, scale));
        MyMatrix rotationTransform = MyMatrix.CreateRotationZ(Mathf.PI / 8);
        MyMatrix comboTransform = localTransform.Multiply(rotationTransform);
        comboTransform = comboTransform.Multiply(scaleTransform);
        GameObject planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        planet.GetComponent<Renderer>().material.color = color;
        if (material != null)
        {
            planet.GetComponent<Renderer>().material = material;
        }
        return new SceneGraphNode("name", comboTransform, planet);
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

