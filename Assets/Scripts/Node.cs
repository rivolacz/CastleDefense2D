using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Node
    {
        public Vector3Int position;
        public bool isObstacle;
        public List<Node> neighbors;
        public int GCost;
        public int HCost;
        public Node Parent;

        public int FCost
        {
            get
            {
                return GCost + HCost;
            }
        }

        public Node(Vector3Int position, bool isObstacle)
        {
            this.position = position;
            this.isObstacle = isObstacle;
            this.neighbors = new List<Node>();
        }
    }
}
