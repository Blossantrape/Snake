using UnityEngine;

namespace General
{
    public class VFXController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _vfxObject;
        private static VFXController _instance;

        private void Awake()
        {
            _instance = this;
        }

        public void ActivateVFX()
        {
            Debug.Log("vfx - play");
            _vfxObject.Play();
            Debug.Log("vfx - end");
        }

        public void DeactivateVFX()
        {
            _vfxObject.Stop();
        }
    }
}
