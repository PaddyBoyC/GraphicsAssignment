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

        Planet sun = new Planet(new MyVector(0, 0, 0), oneVector.Multiply(5), new MyVector(0, 0, 0), 10000, Color.yellow); ;
        rootScaleNode.AddChild(sun.RootNode);
        bodies.Add(sun);

        Planet mercury = new Planet(new MyVector(5, 0, 0), oneVector.Multiply(0.20f), new MyVector(0, 0, 0), 1, Color.blue, earthMaterial);
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

        Planet jupiter = new Planet(new MyVector(13, 0, 0), oneVector.Multiply(2f), new MyVector(-1, 0, 0), 1, Color.blue, earthMaterial);
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
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, Camera.main.nearClipPlane));
        MyVector mouseWorldPos2 = new MyVector(mouseWorldPos.x, mouseWorldPos.y, mouseWorldPos.z);
        MyVector cameraPos = new MyVector(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        foreach (var body in bodies)
        {
            body.Update(bodies);

            Planet planet = body as Planet;

            if (planet != null && LineSphereIntersection(cameraPos, mouseWorldPos2, body.Position, planet.Scale.X))
            {
                Debug.DrawLine(Camera.main.transform.position, new Vector3(body.Position.X, body.Position.Y, body.Position.Z), Color.red);
            }
        }
        RootNode.Draw(MyMatrix.CreateIdentity());

        
        //Debug.Log(Physics.Raycast(mouseWorldPos, (mouseWorldPos - Camera.main.transform.position)));

        
        Debug.DrawRay(mouseWorldPos, (mouseWorldPos - Camera.main.transform.position) * 100);
    }

    bool LineSphereIntersection(MyVector line1, MyVector line2, MyVector sphereOrigin, float sphereRadius)
    {
        //float a = Mathf.Pow(line2.X - line1.X, 2) + Mathf.Pow(line2.Y - line1.Y, 2) + Mathf.Pow(line2.Z - line1.Z, 2);
        //float b = -2 * ((line2.X - line1.X) * (sphereOrigin.X - line1.X) + (line2.Y - line1.Y) * (sphereOrigin.Y - line1.Y) + (sphereOrigin.Z - line1.Z) * (line2.Z - line1.Z));
        //float c = Mathf.Pow(sphereOrigin.X - line1.X, 2) + Mathf.Pow(sphereOrigin.Y - line1.Y, 2) + Mathf.Pow(sphereOrigin.Z - line1.Z, 2) - (sphereRadius * sphereRadius);
        //return b * b - (4 * a * c) > 0;

        MyVector u = line2.Subtract(line1).Normalise();
        float left = Mathf.Pow(u.DotProduct(line1.Subtract(sphereOrigin)), 2);
        float right = line1.Subtract(sphereOrigin).MagnitudeSq() - sphereRadius * sphereRadius;
        float result = left - right;
        return result > 0;
    }
}

