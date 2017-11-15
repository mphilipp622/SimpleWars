using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {
    public AudioClip Loop1, Loop2;
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
        if (turn == 5)
        {
            das.CrossFade(Loop1, 1, 10, 0);
        }
        else if (turn == 10)
        {
            das.CrossFade(Loop2, 1, 10, 0);
        }
    }
}
