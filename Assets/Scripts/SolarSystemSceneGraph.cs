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

        Planet sun = new Planet(new MyVector(0, 0, 0), oneVector.Multiply(5), new MyVector(0, 0, 0), 10000, Color.yellow);;
        rootScaleNode.AddChild(sun.RootNode);
        bodies.Add(sun);

        Planet mercury = new Planet(new MyVector(5, 0, 0), oneVector.Multiply(0.20f), new MyVector(0, 0, 10), 1, Color.blue, earthMaterial);
        rootScaleNode.AddChild(mercury.RootNode);
        bodies.Add(mercury);

        Planet venus = new Planet(new MyVector(6, 0, 0), oneVector.Multiply(0.50f), new MyVector(0, 0, 3), 1, Color.blue, earthMaterial);
        rootScaleNode.AddChild(venus.RootNode);
        bodies.Add(venus);

        Planet earth = new Planet(new MyVector(7, 0, 0), oneVector.Multiply(0.50f), new MyVector(0, 0, 3), 1, Color.blue, earthMaterial);
        rootScaleNode.AddChild(earth.RootNode);
        bodies.Add(earth);

        Planet moon = new Planet(new MyVector(7.6f, 0, 0), oneVector.Multiply(0.15f), new MyVector(0, 0, 3), 0.01f, Color.gray);
        rootScaleNode.AddChild(moon.RootNode);
        bodies.Add(moon);

        Planet mars = new Planet(new MyVector(10, 0, 0), oneVector.Multiply(0.50f), new MyVector(0, 0, 3), 1, Color.blue, earthMaterial);
        rootScaleNode.AddChild(mars.RootNode);
        bodies.Add(mars);

        Planet jupiter = new Planet(new MyVector(13, 0, 0), oneVector.Multiply(2f), new MyVector(0, 0, 3), 1, Color.blue, earthMaterial);
        rootScaleNode.AddChild(jupiter.RootNode);
        bodies.Add(jupiter);

        Planet saturn = new Planet(new MyVector(18, 0, 0), oneVector.Multiply(1.5f), new MyVector(0, 0, 3), 1, Color.blue, earthMaterial);
        rootScaleNode.AddChild(saturn.RootNode);
        bodies.Add(saturn);

        Planet uranus = new Planet(new MyVector(26, 0, 0), oneVector.Multiply(1f), new MyVector(0, 0, 3), 1, Color.blue, earthMaterial);
        rootScaleNode.AddChild(uranus.RootNode);
        bodies.Add(uranus);

        Planet neptune = new Planet(new MyVector(28, 0, 0), oneVector.Multiply(1f), new MyVector(0, 0, 3), 1, Color.blue, earthMaterial);
        rootScaleNode.AddChild(neptune.RootNode);
        bodies.Add(neptune);

        Satellite satellite = new Satellite(new MyVector(2, 0, 3), oneVector.Multiply(0.1f), new MyVector(1, 1, 1), 0.01f, Color.gray);
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

