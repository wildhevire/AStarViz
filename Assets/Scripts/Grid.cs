using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wildhevire.AStarViz
{
    public class Grid : MonoBehaviour
    {
        public GridProperties properties;
        public GameObject nodePrefab;
        public Node[,] Nodes;

        [SerializeField] private float offset = .5f;

        private void Awake()
        {
            CreateGrid();
        }

        private void Start()
        {

        }

        private void CreateGrid()
        {
            var pivotPos = Vector3.zero;
            Nodes = new Node[properties.dimension.x, properties.dimension.y];
            for (int i = 0; i < properties.dimension.x; i++)
            {
                for (int j = 0; j < properties.dimension.y; j++)
                {
                    var newGO = Instantiate(nodePrefab);
                    var vectorOffset = Vector3.one * offset;
                    newGO.transform.position = pivotPos + (Vector3.right * i + vectorOffset) + (Vector3.forward * j + vectorOffset);
                    Node node = newGO.GetComponent<Node>();
                    node.gridPosition = new Vector2Int(i, j);
                    node.State = Node.NodeState.Open;
                    node.isWalkable = true;
                    if (i == 0 || j == 0 || i == properties.dimension.x - 1 || j == properties.dimension.y - 1)
                    {
                        //node.isWalkable = false;
                    }
                    else
                    {
                        //node.isWalkable = false;
                    }
                    Nodes[i,j] = node;
                }
            }
            SetAllNodeNeighbor();

            
        }

        public void SetAllNodeNeighbor()
        {
            for (int i = 0; i < properties.dimension.x; i++)
            {
                for (int j = 0; j < properties.dimension.y; j++)
                {
                    SetNeighbours(Nodes[i, j]);
                }
            }
        }

        private void SetNeighbours(Node node)
        {
            if (node.gridPosition.x - 1 >= 0 && Nodes[node.gridPosition.x - 1, node.gridPosition.y].isWalkable)
            {
                node.AddNeighbor(Nodes[node.gridPosition.x - 1  , node.gridPosition.y]);
            }
            if (node.gridPosition.y - 1 >= 0 && Nodes[node.gridPosition.x, node.gridPosition.y - 1].isWalkable)
            {
                node.AddNeighbor(Nodes[node.gridPosition.x      , node.gridPosition.y - 1]);
            }
            if (node.gridPosition.x + 1 < properties.dimension.x && Nodes[node.gridPosition.x + 1, node.gridPosition.y].isWalkable)
            {
                node.AddNeighbor(Nodes[node.gridPosition.x + 1  , node.gridPosition.y]);
            }
            if (node.gridPosition.y + 1 < properties.dimension.y && Nodes[node.gridPosition.x, node.gridPosition.y + 1].isWalkable)
            {
                node.AddNeighbor(Nodes[node.gridPosition.x      , node.gridPosition.y + 1]);
            }

        }


    }

}
