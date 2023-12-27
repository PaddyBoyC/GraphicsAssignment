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
            SceneGraphNode planetGeometry = new SceneGraphNode("PlanetGeometry", MyMatrix.CreateIdentity(), planet);
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
    }
}
