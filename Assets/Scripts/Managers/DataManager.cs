using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager 
{
    #region Volume
    float _BGMVolume;
    public float BGMVolume { get { return _BGMVolume; } set { PlayerPrefs.SetFloat("BGMVolume", value); _BGMVolume = value; } }
    
    float _SFXVolume;
    public float SFXVolume { get { return _SFXVolume; } set { PlayerPrefs.SetFloat("SFXVolume", value); _SFXVolume = value; } }

    #endregion Volume


    public void Init()
    {
        _BGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        _SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

    }

}
