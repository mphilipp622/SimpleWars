using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {
    public AudioClip Loop1, Loop2, Loop3, Loop4, Loop5;
    public DoubleAudioSource das;
    AudioSettings settings;
    private void Awake()
    {
        das = GetComponent<DoubleAudioSource>();
    }
    // Use this for initialization
    void Start () {
        settings = GameObject.FindGameObjectWithTag("SoundSettings").GetComponent<AudioSettings>();
    }
	
	// Update is called once per frame
	void Update () {

	}
    
    public void SwitchMusic(int turn)
    {
        float vol = settings.Loudness("bgm");
        if (turn == 10)
        {
            das.CrossFade(Loop1, vol, 10, 0);
        }
        else if (turn == 20)
        {
            das.CrossFade(Loop2, vol, 10, 0);
        }
        else if (turn == 60)
        {
            das.CrossFade(Loop3, vol, 10, 0);
        }
        else if (turn == 90)
        {
            das.CrossFade(Loop4, vol, 10, 0);
        }
        else if (turn == 120)
        {
            das.CrossFade(Loop5, vol, 10, 0);
        }
    }
}
