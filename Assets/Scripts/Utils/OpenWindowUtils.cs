using UnityEngine;

namespace Scripts.Utils
{
    public static class OpenWindowUtils 
    {
        public static void CreateWindow(string windowPath)
        {
            var window = Resources.Load<GameObject>(windowPath);
            var canvas = Object.FindObjectOfType<Canvas>();
            Object.Instantiate(window, canvas.transform);
        }
    }
}
