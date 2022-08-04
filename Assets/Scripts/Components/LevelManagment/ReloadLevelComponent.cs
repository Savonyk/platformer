using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Model;

namespace Scripts.Components.LevelManagment
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void OnReloadScene()
        {
            var session = FindObjectOfType<GameSession>();
            session.LoadLastSave();

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}