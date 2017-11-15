using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {
    public AudioClip Loop1, Loop2, Loop3, Loop4, Loop5;
    public DoubleAudioSource das;
    private void Awake()
    {
        das = GetComponent<DoubleAudioSource>();
    }
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

	}
    
    public void SwitchMusic(int turn)
    {
        if (turn == 10)
        {
            das.CrossFade(Loop1, 1, 10, 0);
        }
        else if (turn == 20)
        {
            das.CrossFade(Loop2, 1, 10, 0);
        }
        else if (turn == 60)
        {
            das.CrossFade(Loop3, 1, 10, 0);
        }
        else if (turn == 90)
        {
            das.CrossFade(Loop4, 1, 10, 0);
        }
        else if (turn == 120)
        {
            das.CrossFade(Loop5, 1, 10, 0);
        }
    }
}
