using System.Collections.Generic;
using UnityEngine;
namespace Wildhevire.AStarViz
{
    public class Node : MonoBehaviour
    {
        public Vector2Int gridPosition;
        private Dictionary<Node, int> neighbors = new Dictionary<Node, int>();
        public enum NodeState
        {
            Start,
            Goal,
            Open,
            Explored,
            Chosen
        }

        public NodeState State;
        public int gCost;
        public int hCost;
        public bool isWalkable;

        public int FCost { get { return gCost + hCost; } }



        public Dictionary<Node, int> Neighbors
        {
            get { return neighbors; }
        }

        public void AddNeighbor(Node node, int edgeLength)
        {
            if (!neighbors.ContainsKey(node))
                neighbors.Add(node, edgeLength);
        }



        public void AddNeighbor(Node other)
        {
            var distance = Vector3.Distance(other.transform.position, transform.position);
            AddNeighbor(other, (int)distance);
        }

        public void DebugNeighbors()
        {
            string sentences = $"This Node ({gridPosition.x}, {gridPosition.y}) has Neighbors : \n";
            foreach(var n in neighbors)
            {
                var current = n.Key;
                var dist = n.Value;
                sentences += $"({current.gridPosition.x}, {current.gridPosition.y}) : {dist} \n";
            }
            Debug.Log(sentences);
        }

    }
}

