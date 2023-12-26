using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

namespace Assets.Scripts
{
    internal class Planet
    {
        public SceneGraphNode RootNode { get; private set; }
        private SceneGraphNode PositionNode { get; set; }
        private SceneGraphNode RotationNode { get; set; }
        private SceneGraphNode ScaleNode { get; set; }
        public MyVector Position { get; private set; }
        public MyVector Scale { get; set; }
        public MyVector Velocity { get; private set; }
        public float Mass { get; private set; }

        public Planet(MyVector pPosition, MyVector pScale,MyVector pVelocity, float pMass, Color pColour, Material material = null)
{
            Position = pPosition;
            Scale = pScale;
            Velocity = pVelocity;
            Mass = pMass;
            initialiseSceneGraph(pColour, material);
        }

        private void initialiseSceneGraph(Color pColour, Material material = null)
        {
            MyVector translationVector = Position;
            MyMatrix translationMatrix = MyMatrix.CreateTranslation(translationVector);
            PositionNode = new SceneGraphNode("PlanetPosition", translationMatrix);

            MyMatrix scaleMatrix = MyMatrix.CreateScale(Scale);
            ScaleNode = new SceneGraphNode("PlanetScale", scaleMatrix);
            PositionNode.AddChild(ScaleNode);

            MyMatrix rotationMatrix = MyMatrix.CreateRotationZ(Mathf.PI / 8);
            RotationNode = new SceneGraphNode("PlanetRotation", rotationMatrix);
            ScaleNode.AddChild(RotationNode);

            GameObject planet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            planet.GetComponent<Renderer>().material.color = pColour;
            if (material != null)
            {
                planet.GetComponent<Renderer>().material = material;
            }
            SceneGraphNode planetGeometry = new SceneGraphNode("PlanetGeometry", MyMatrix.CreateIdentity(), planet);
            RotationNode.AddChild(planetGeometry);

            RootNode = PositionNode;
        }

        public void Update(List<Planet> planets)
        {
            foreach (var planet in planets)
            {
                if (planet != this)
                {
                    MyVector v = Position.Subtract(planet.Position);
                    float distance = v.Magnitude();
                    const float G = 0.0001f;
                    float force = ((G * Mass * planet.Mass) / (distance * distance))/Mass;
                    Velocity = Velocity.Add(new MyVector(0, 0, 0).Subtract(v).Normalise().Multiply(force));
                }
            }
            Position = Position.Add(Velocity.Multiply(Time.deltaTime));
            //Position = Position.Add(new MyVector(Time.deltaTime * 0.1f, 0, 0));
            PositionNode.Transform = MyMatrix.CreateTranslation(Position);
            RotationNode.Transform = RotationNode.Transform.Multiply(MyMatrix.CreateRotationY(Time.deltaTime * 1f));
        }
    }
}
