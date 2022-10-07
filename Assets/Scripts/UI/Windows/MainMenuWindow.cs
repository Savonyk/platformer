using System;
using UnityEngine;
using Scripts.Utils;
using UnityEngine.SceneManagement;

namespace Scripts.UI.Windows
{
    public class MainMenuWindow : AnimatedWindow
    {
        private Action _closeAction;

        public void OnShowSetting()
        {
            OpenWindowUtils.CreateWindow("UI/SettingsWindow");
        }

        public void OnStartGame()
        {
            _closeAction = () => 
            { 
                SceneManager.LoadScene("Level1"); 
            };
            Close();
        }

        public void OnExit()
        {
            _closeAction = () =>
            {
                Application.Quit();

#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            };
            Close();
        }

        public override void OnCloseAnimationCompleted()
        {
            _closeAction?.Invoke();

            base.OnCloseAnimationCompleted();
            
        }
    }
}
