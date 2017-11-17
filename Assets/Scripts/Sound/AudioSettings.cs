using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour {
    public Slider master;
    public Slider sfx;
    public Slider bgm;

    public float mt, st, bt;

    void Awake()
    {
        mt = PlayerPrefs.GetFloat("mt");
        st = PlayerPrefs.GetFloat("st");
        bt = PlayerPrefs.GetFloat("bt");
        master.value = mt;
        sfx.value = st;
        bgm.value = bt;
        Debug.Log(st);
        Debug.Log(bt);
    }
    void Update()
    {
        master.value = mt;
        sfx.value = st;
        bgm.value = bt;
    }
    public void GetMasterVolume()
    {
        mt = master.value;
        if(st>mt)
        {
            st = st * mt;
        }
        if (bt > mt)
        {
            bt = bt * mt;
        }
        Debug.Log(mt);
        Debug.Log(st);
        Debug.Log(bt);
    }
    public void GetSfxVolume()
    {
        st = sfx.value;
        if (st > mt)
        {
            mt = st;
            master.value = st;
        }
            
    }
    public void GetBgmVolume()
    {
        bt = bgm.value;
        if (bt > mt)
        {
            mt = bt;
            master.value = bt;
        }
    }
    public float Loudness(string type)
    {
        if (type == "bgm")
            return mt * bt;
        else if (type == "sfx")
            return mt * st;
        else
            return 1;
    }
    public void back()
    {
        PlayerPrefs.SetFloat("mt", mt);
        PlayerPrefs.SetFloat("st", st);
        PlayerPrefs.SetFloat("bt", bt);
        Application.LoadLevel(2);
    }
}
