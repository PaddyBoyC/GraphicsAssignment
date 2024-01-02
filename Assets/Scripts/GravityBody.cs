using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    internal abstract class GravityBody
    {
        public SceneGraphNode RootNode { get; protected set; }
        public MyVector Position { get; protected set; }
        public float Mass { get; protected set; }
        public MyVector Velocity { get; protected set; }
        protected SceneGraphNode PositionNode { get; set; }

        public virtual void Update(List<GravityBody> bodies)
        {
            foreach (var otherBody in bodies)
            {
                if (otherBody != this)
                {
                    MyVector v = Position.Subtract(otherBody.Position);
                    float distance = v.Magnitude();
                    const float G = 0.0000001f;
                    float force = ((G * Mass * otherBody.Mass) / (distance * distance)) / Mass;
                    Velocity = Velocity.Add(new MyVector(0, 0, 0).Subtract(v).Normalise().Multiply(force));



                    if (Collides(otherBody, distance))
                    {
                        Debug.Log("collided");
                        CollisionResponse(otherBody, distance, v);
                    }
                }
            }
            Position = Position.Add(Velocity.Multiply(Time.deltaTime));
            PositionNode.Transform = MyMatrix.CreateTranslation(Position);
        }

        protected abstract bool Collides(GravityBody otherBody, float distance);
        protected abstract void CollisionResponse(GravityBody otherBody, float distance, MyVector v);
    }
}
