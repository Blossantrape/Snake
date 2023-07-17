using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLoader
{
    public static class Loader
    {
        public enum Scene {
            GameScene,
            Loading,
            MainMenu,
        }

        private static Action _loaderCallbackAction;
    
        public static void Load(Scene scene)
        {
            Time.timeScale = 1f;
            // Лямбда выражение, разобрать его.
            _loaderCallbackAction = () =>
            {
                SceneManager.LoadScene(scene.ToString());
            };
            
            SceneManager.LoadScene(Scene.Loading.ToString());
        }

        public static void LoaderCallback() {
            if (_loaderCallbackAction != null) {
                _loaderCallbackAction();
                _loaderCallbackAction = null;
            }
        }
    }
}
