using System;
using General;
using UnityEngine;

namespace DebugPlus
{
    public class DebugPlusScript : MonoBehaviour
    {
        private DebugPlusScript _instance;
        [SerializeField] private VFXController _vfxController;

        private void Awake()
        {
            _instance = this;
        }

        public void DebugWinSound()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                SoundManager.PlaySound(SoundManager.Sound.WinGame);
            }
        }

        public void DebugWinGameScore()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Score.DebugScore();
                Debug.Log("271");
            }
        }

        public void DebugVFXWinGame()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _vfxController.ActivateVFX();
            }
        }

        public void DebugStopGameBackgroundMusic()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                SoundManager.StopGameSceneMusic();
                Debug.Log("P");
            }
        }
    }
}