using System;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool _firstUpdate = true;

    private void Update()
    {
        if (_firstUpdate) {
            _firstUpdate = false;
            Loader.LoaderCallback();
        }
    }
}
