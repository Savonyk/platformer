using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Model;

namespace Scripts.Components.LevelManagment
{
    class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] 
        private string _sceneName;

        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();
            SceneManager.LoadScene(_sceneName);
        }
    }
}
