﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI.MainMenu
{
    public class MainMenuWindow : AnimatedWindow
    {
        private Action _closeAction;

        public void OnShowSetting()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
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
