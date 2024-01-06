using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem
{
    MyVector Position { get; set; }
    MyVector Rotation { get; set; }
    MyVector Scale { get; set; }

    public SolarSystem(MyVector pPosition, MyVector pRotation, MyVector pScale, Material earthMaterial)
    {
        Position = pPosition;
        Rotation = pRotation;
        Scale = pScale;

        MyMatrix parentMatrix = MyMatrix.CreateTranslation(Position);

        GameObject sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sun.GetComponent<Renderer>().material.color = Color.yellow;


        // Old way of calling a the object to the scene, not really of any use of anymore

        //MyVector localTranslation = new MyVector(0, 0, 0);
        //MyMatrix bottomTransformMatrix = MyMatrix.CreateTranslation(localTranslation);
        //bottomTransformMatrix = bottomTransformMatrix.Multiply(parentMatrix);
        //bottomTransformMatrix = bottomTransformMatrix.Multiply(MyMatrix.CreateScale(new MyVector(busWidth, 1, busLength)));
        //bottomTransformMatrix.SetTransform(sun);

        MyMatrix earthTransform = BuildPlanet(parentMatrix, new MyVector(5, 0 , 0), Color.blue, 0.25f, earthMaterial);
        BuildPlanet(earthTransform, new MyVector(0, 0, 2f), Color.gray, 0.25f);

    }
    
    //Builds the planets

    private MyMatrix BuildPlanet(MyMatrix pParentTransform, MyVector pOffset, Color color, float scale, Material material = null)
    { 
        MyMatrix localTransform = MyMatrix.CreateTranslation(pOffset);
        MyMatrix scaleTransform = MyMatrix.CreateScale(new MyVector(scale, scale, scale));
        MyMatrix rotationTransform = MyMatrix.CreateRotationZ(Mathf.PI / 8);
        MyMatrix comboTransform = pParentTransform.Multiply(localTransform);
        comboTransform = comboTransform.Multiply(rotationTransform);
        comboTransform = comboTransform.Multiply(scaleTransform);
        GameObject planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        planet.GetComponent<Renderer>().material.color = color;
        if (material != null)
        {
            planet.GetComponent<Renderer>().material = material;
        }
        comboTransform.SetTransform(planet);
        return comboTransform;
    }
}
