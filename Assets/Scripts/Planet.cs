using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;

namespace Assets.Scripts
{
    internal class Planet : GravityBody
    {
        private SceneGraphNode RotationNode { get; set; }
        private SceneGraphNode ScaleNode { get; set; }
        public MyVector Scale { get; set; }

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

            // scale of 2 is to correct for the unity sphere having a radius of 0.5
            SceneGraphNode planetGeometry = new SceneGraphNode("PlanetGeometry", MyMatrix.CreateScale(new MyVector(2, 2, 2)), planet);
            RotationNode.AddChild(planetGeometry);

            RootNode = PositionNode;
        }

        public override void Update(List<GravityBody> bodies)
        {
            base.Update(bodies);
            RotationNode.Transform = RotationNode.Transform.Multiply(MyMatrix.CreateRotationY(Time.deltaTime * 1f));
        }

        protected override bool Collides(GravityBody otherBody, float distance)
        {
            Planet otherPlanet = otherBody as Planet;
            if (otherPlanet != null)
            {
                float myRadius = Scale.X;
                float otherRadius = otherPlanet.Scale.X;
                return (myRadius + otherRadius) > distance;
            }
            return false;
        }

        protected override void CollisionResponse(GravityBody otherBody, float distance, MyVector v)
        {
            Planet otherPlanet = otherBody as Planet;
            if (otherPlanet != null)
            {
                float myRadius = Scale.X;
                float otherRadius = otherPlanet.Scale.X;
                float penetrationDistance = (myRadius + otherRadius) - distance;
                Position = Position.Add(v.Multiply(0.5f * penetrationDistance));
                otherPlanet.Position = otherPlanet.Position.Subtract(v.Multiply(0.5f * penetrationDistance));

                float m1 = Mass;
                float m2 = otherPlanet.Mass;
                MyVector inverseV = v.Multiply(-1);
                MyVector velDelta = Velocity.Subtract(otherPlanet.Velocity);
                MyVector inverseVelDelta = velDelta.Multiply(-1);

                Velocity = Velocity.Subtract(v.Multiply((2 / m2) / (m1 + m2) * velDelta.DotProduct(v) / v.MagnitudeSq()));
                otherPlanet.Velocity = otherPlanet.Velocity.Subtract(inverseV.Multiply((2 / m1) / (m1 + m2) * inverseVelDelta.DotProduct(inverseV) / inverseV.MagnitudeSq()));
            }
        }
    }
}
