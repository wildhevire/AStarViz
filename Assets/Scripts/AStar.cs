using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Wildhevire.AStarViz
{
	public class AStar : MonoBehaviour
	{

		public AgentTest test;
		private Node FindNextNode(Node current, HashSet<Node> openSet)
		{
			var lowest = int.MaxValue;
			Node selectedNode = current;

			foreach (var node in openSet)
			{
				if (node.FCost < lowest || node.FCost == current.FCost)
				{
					if (node.hCost < current.hCost)
                    {
						selectedNode = node;
						lowest = node.FCost;
                    }
				}
			}
			return selectedNode;
		}

		private int Heuristic(Node a, Node b)
		{
			var dx = Mathf.Abs(a.gridPosition.x - b.gridPosition.x);
			var dy = Mathf.Abs(a.gridPosition.y - b.gridPosition.y);
			return 1 * (dx + dy);
		}

		private List<Node> TracePath(Dictionary<Node, Node> cameFrom, Node current)
		{
			List<Node> path = new List<Node>();
			path.Add(current);

			while (cameFrom.ContainsKey(current))
			{
				current = cameFrom[current];
				path.Add(current);
				if (current.State != Node.NodeState.Start && current.State != Node.NodeState.Goal)
				{
					current.State = Node.NodeState.Chosen;
				}
			}
			return path;
		}
		public IEnumerator FindPath(Node start, Node target)
        {
			var closedSet = new HashSet<Node>();
			var openSet = new HashSet<Node>();
			var cameFrom = new Dictionary<Node, Node>();
			
			openSet.Add(start);

			while (openSet.Count > 0)
            {
				var current = FindNextNode(openSet.First(), openSet);

				if (current.State != Node.NodeState.Start && current.State != Node.NodeState.Goal)
				{
					
					current.State = Node.NodeState.Explored;
				}
                if (current == target)
                {
					var foundPath = TracePath(cameFrom, current);
					foundPath.Reverse();
					test.path = foundPath;
					test.canMove = true;
					StopAllCoroutines();
					break;
                }

				openSet.Remove(current);
				closedSet.Add(current);

                foreach (var neighbor in current.Neighbors.Keys)
                {
                    if (!neighbor.isWalkable || closedSet.Contains(neighbor))
                    {
                        continue;
                    }
                    int newCostToNeighbor = current.gCost + current.Neighbors[neighbor];
                    if (newCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newCostToNeighbor;
                        neighbor.hCost = Heuristic(neighbor, target);
						cameFrom[neighbor] = current;

						neighbor.gameObject.GetComponent<NodeBehaviour>().SetCostProperties();
						if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                    }
                }

				

				yield return new WaitForSeconds(.1f);
			}
		}

		
	}
}

