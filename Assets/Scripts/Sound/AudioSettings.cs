using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioSettings : MonoBehaviour {
    public Slider master;
    public Slider sfx;
    public Slider bgm;

    public float masterVolume = 1, sfxVolume = 1, bgmVolume = 1;

    public void SetMasterVolume()
    {
        masterVolume = master.value;
        Debug.Log(masterVolume);
    }
    public void SetSfxVolume()
    {
        sfxVolume = sfx.value;
    }
    public void SetBgmVolume()
    {
        bgmVolume = bgm.value;
    }
    public float Loudness(string type)
    {
        if (type == "bgm")
            return masterVolume * bgmVolume;
        else if (type == "sfx")
            return masterVolume * sfxVolume;
        else
            return 1;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
