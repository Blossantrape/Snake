using System;
using UnityEngine;
using UnityEngine.VFX;

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
        _vfxObject.Play();
    }

    public void DeactivateVFX()
    {
        _vfxObject.Stop();
    }
}
