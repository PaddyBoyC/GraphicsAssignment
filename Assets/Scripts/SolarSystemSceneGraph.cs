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

    List<GravityBody> bodies = new List<GravityBody>();

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

        Planet sun = new Planet(new MyVector(0, 0, 0), oneVector, new MyVector(0, 0, 0), 1000, Color.yellow);;
        rootScaleNode.AddChild(sun.RootNode);
        bodies.Add(sun);

        Planet earth = new Planet(new MyVector(5, 0, 0), oneVector.Multiply(0.25f), new MyVector(0, 0, 3), 1, Color.blue, earthMaterial);
        rootScaleNode.AddChild(earth.RootNode);
        bodies.Add(earth);

        Planet moon = new Planet(new MyVector(5.6f, 0, 0), oneVector.Multiply(0.25f), new MyVector(0, 0, 3), 0.01f, Color.gray);
        rootScaleNode.AddChild(moon.RootNode);
        bodies.Add(moon);

        Satellite satellite = new Satellite(new MyVector(5, 0, 3), oneVector.Multiply(0.1f), new MyVector(1, 1, 1), 0.01f, Color.gray);
        rootScaleNode.AddChild(satellite.RootNode);
        bodies.Add(satellite);

        RootNode.Draw(MyMatrix.CreateIdentity());
    }
    
    public void Update()
    {
        foreach (var body in bodies)
        {
            body.Update(bodies);
        }
        RootNode.Draw(MyMatrix.CreateIdentity());
    }
}

