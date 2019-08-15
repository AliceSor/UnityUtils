using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PersistentData
{
    public bool ApplicationFirstLoad;
    public bool fullscreen;
    [Range(0, 1)]
    public float soundVolume;
    [Range(0, 1)]
    public float musicVolume;
    public string userName;
}
