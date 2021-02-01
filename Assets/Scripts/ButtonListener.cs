using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wildhevire.AStarViz.UI
{
    public class ButtonListener : MonoBehaviour
    {
        public void OnReset()
        {
            SceneManager.LoadScene(0);
        }
    }
}
