using System;
using UnityEngine;

namespace General
{
    public class MyDontDestroyOnLoad : MonoBehaviour
    {
        private static MyDontDestroyOnLoad _instance;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}