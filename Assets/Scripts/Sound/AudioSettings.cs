using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioSettings : MonoBehaviour {
    public Slider sfx;
    public Slider bgm;
    bool masterBool = false;
    public float mt, st, bt;

    void Awake()
    {
        st = PlayerPrefs.GetFloat("st");
        bt = PlayerPrefs.GetFloat("bt");
        sfx.value = st;
        bgm.value = bt;
    }
    void Update()
    {

    }

    public void GetSfxVolume()
    {
        st = sfx.value;
            
    }
    public void GetBgmVolume()
    {
        bt = bgm.value;
 
    }

    public void back()
    {
        PlayerPrefs.SetFloat("st", st);
        PlayerPrefs.SetFloat("bt", bt);
        Application.LoadLevel(0);
    }
}
