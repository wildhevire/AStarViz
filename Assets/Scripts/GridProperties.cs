using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wildhevire.AStarViz
{
    [CreateAssetMenu( fileName = "GridProperties.asset", menuName = "Wildhevire/Grid Properties")]
    public class GridProperties : ScriptableObject
    {
        [SerializeField] public Vector2Int dimension;
    }
}

