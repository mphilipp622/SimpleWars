using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {
    public AudioClip Loop1, Loop2, Loop3, Loop4, Loop5, Loop6;
    public DoubleAudioSource das;
    AudioSettings settings;
    float volume;
    bool f = true;
    private void Awake()
    {
        das = GetComponent<DoubleAudioSource>();
        volume = PlayerPrefs.GetFloat("bt");
    }
    // Use this for initialization
    void Start () {
        //settings = GameObject.FindGameObjectWithTag("SoundSettings").GetComponent<AudioSettings>();
    }
	
	// Update is called once per frame
	void Update () {
        if (f)
        {
            f = false;
            das.setVolume(volume); //shit wont work in Awake() or Start(); stupid cunt fucking shit
        }
    }

    public void SwitchMusic(int turn)
    {
        if (turn == 5)
        {
            das.CrossFade(Loop1, volume, 10, 0);
        }
        else if (turn == 10)
        {
            das.CrossFade(Loop2, volume, 10, 0);
        }
        else if (turn == 15)
        {
            das.CrossFade(Loop3, volume, 10, 0);
        }
        else if (turn == 20)
        {
            das.CrossFade(Loop4, volume, 10, 0);
        }
        else if (turn == 25)
        {
            das.CrossFade(Loop5, volume, 10, 0);
        }
    }
}
