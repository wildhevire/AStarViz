using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Wildhevire.AStarViz
{
    public class ClickHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Grid grid;
        [SerializeField] private GridProperties properties;
        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                var node = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Node>();
                var behaviour = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<NodeBehaviour>();
                node.isWalkable= !node.isWalkable;
                behaviour.ToogleObstacle(node.isWalkable);
                //node.DebugNeighbors();
            }
        }
        private void Awake()
        {
            grid = GameObject.FindObjectOfType<Grid>();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        
    }
}

