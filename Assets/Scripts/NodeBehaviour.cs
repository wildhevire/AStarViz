using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Wildhevire.AStarViz
{
    public class NodeBehaviour : MonoBehaviour
    {
        public GridProperties properties;
        public GameObject obstacle;
        public Canvas canvas;
        public TMP_Text gCostText;
        public TMP_Text hCostText;
        public TMP_Text fCostText;
        private Node node;
        public SpriteRenderer material;

        private void Start()
        {
            
            node = GetComponent<Node>();
            //ToogleObstacle(node.isWalkable);
            fCostText.text = $"({node.gridPosition.x}, {node.gridPosition.y})";
            if (node.FCost == 0) canvas.gameObject.SetActive(false);
        }

        private void Update()
        {
            SetColor();
        }

        public void ToogleObstacle(bool status)
        {
            obstacle.SetActive(!status);
            //canvas.gameObject.SetActive(status);
            
        }

        public void SetCostProperties(int gCost, int hCost, int fCost)
        {
            canvas.gameObject.SetActive(true);
            gCostText.text = gCost.ToString();
            hCostText.text = hCost.ToString();
            fCostText.text = fCost.ToString();
        }

        public void SetCostProperties()
        {
            SetCostProperties(node.gCost, node.hCost, node.FCost);
        }

        void SetColor()
        {
            switch (node.State)
            {
                case Node.NodeState.Explored:
                    {
                        material.color = Color.green;
                        break;
                    }
                case Node.NodeState.Chosen:
                    {
                        material.color = Color.yellow;
                        break;
                    }
                case Node.NodeState.Start:
                    {
                        material.color = Color.black;
                        break;
                    }
                case Node.NodeState.Goal:
                    {
                        material.color = Color.blue;
                        break;
                    }
            }
        }

    }

}
