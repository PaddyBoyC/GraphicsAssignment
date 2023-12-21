using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class SceneGraphNode
    {
        public string Name { get; private set; }

        public MyMatrix Transform { get; set; }

        public GameObject gameObject { get; private set; }

        List<SceneGraphNode> children = new List<SceneGraphNode>();

        public SceneGraphNode(string pName, MyMatrix pTransform, GameObject pGameObject = null)
        {
            Name = pName;
            Transform = pTransform;
            gameObject = pGameObject;
        }

        public int GetNumberOfChildren()
        {
            return children.Count;
        }

        public SceneGraphNode GetChildAt(int pIndex)
        {
            return children[pIndex];
        }

        public void AddChild(SceneGraphNode pChildNode)
        {
            children.Add(pChildNode);
        }

        public void Draw(MyMatrix pParentTransform)
        {
            MyMatrix transform = pParentTransform.Multiply(Transform);
            if (gameObject != null)
            {
                transform.SetTransform(gameObject);
            }

            foreach (SceneGraphNode child in children)
            {
                child.Draw(transform);
            }
        }
    }
}
