using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wildhevire.AStarViz
{
    public class AgentTest : MonoBehaviour
    {
        public List<Node> path = new List<Node>();
        public AStar finding;
        public Grid grid;
        public Node start;
        public Node target;
        public IEnumerator coroutine;
        public bool canMove = false;
        public int pathIndex = 0;
        public Vector2Int startCoord;
        public Vector2Int targetCoord;
        public Animator anim;
        private void Awake()
        {

        }
        private void Start()
        {
            grid = FindObjectOfType<Grid>();
            start = grid.Nodes[startCoord.x, startCoord.y];
            target = grid.Nodes[targetCoord.x, targetCoord.y];
            start.State = Node.NodeState.Start;
            target.State = Node.NodeState.Goal;
            coroutine = finding.FindPath(start, target);
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(coroutine);
            }
            if (path.Count == 0) transform.position = start.transform.position;

            if (canMove)
            {
                anim.SetBool("isMoving", canMove);
                if (path.Count == 0) return;
                if (pathIndex + 1 > path.Count - 1)
                {
                    //pathIndex = 0;
                    canMove = false;
                    anim.SetBool("isMoving", canMove);
                    return;
                }
                Move();
                if (transform.position == path[pathIndex + 1].transform.position)
                {
                    //transform.rotation = Quaternion.LookRotation(path[pathIndex + 1].transform.position, transform.up);
                    pathIndex += 1;
                }

                
            }
        }


        private void Move()
        {
            if (pathIndex + 1 > path.Count - 1) return;
            transform.LookAt(path[pathIndex + 1].transform.position, transform.up);
            transform.position = Vector3.MoveTowards(transform.position, path[pathIndex + 1].transform.position,
               2 * Time.deltaTime);

        }

    }
}