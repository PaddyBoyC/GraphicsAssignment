using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Satellite : GravityBody
    {
        private SceneGraphNode RotationNode { get; set; }
        private SceneGraphNode ScaleNode { get; set; }
        public MyVector Scale { get; set; }

        public Satellite(MyVector pPosition, MyVector pScale, MyVector pVelocity, float pMass, Color pColour, Material material = null)
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
            PositionNode = new SceneGraphNode("SatellitePosition", translationMatrix);

            MyMatrix scaleMatrix = MyMatrix.CreateScale(Scale);
            ScaleNode = new SceneGraphNode("SatelliteScale", scaleMatrix);
            PositionNode.AddChild(ScaleNode);

            MyMatrix rotationMatrix = MyMatrix.CreateRotationZ(Mathf.PI / 12);
            RotationNode = new SceneGraphNode("SatelliteRotation", rotationMatrix);
            ScaleNode.AddChild(RotationNode);

            MyMatrix body1ScaleMatrix = MyMatrix.CreateScale(new MyVector(1, 0.1f, 0.5f));
            var body1ScaleNode = new SceneGraphNode("satelliteBody1Scale", body1ScaleMatrix);
            RotationNode.AddChild(body1ScaleNode);

            GameObject body1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            SceneGraphNode body1Node = new SceneGraphNode("satellite1",
                MyMatrix.CreateIdentity(),
                body1);
            body1ScaleNode.AddChild(body1Node);

            MyMatrix body2RotationMatrix = MyMatrix.CreateRotationZ(Mathf.PI / 4);
            var rotationNode2 = new SceneGraphNode("SatelliteRotation2", body2RotationMatrix);
            body1Node.AddChild(rotationNode2);

            GameObject body2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            SceneGraphNode body2Node = new SceneGraphNode("satellite2",
                MyMatrix.CreateIdentity(),
                body2);

            rotationNode2.AddChild(body2Node);

            RootNode = PositionNode;
        }

        public override void Update(List<GravityBody> bodies)
        {
            base.Update(bodies);
            RotationNode.Transform = RotationNode.Transform.Multiply(MyMatrix.CreateRotationY(Time.deltaTime * 2f));
        }

        protected override bool Collides(GravityBody otherBody, float distance)
        {
            return false;
        }
    }
}
