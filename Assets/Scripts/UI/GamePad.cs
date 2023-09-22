using System;
using UnityEngine;

namespace UI
{
    public class GamePad : MonoBehaviour
    {
        private static GamePad _instance;
        
        private void Awake()
        {
            _instance = this;
        }
        
        private void Show() {
            gameObject.SetActive(true);
        }
    
        private void Hide() {
            gameObject.SetActive(false);
        }

        public static void ShowStatic() {
            _instance.Show();
        }

        public static void HideStatic()
        {
            _instance.Hide();
        }
    }
}