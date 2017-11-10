using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSound : MonoBehaviour {
    public AudioClip defaultClip;
    public AudioClip grassSound;
    public AudioClip roadSound;
    public AudioClip mountainSound;
    public AudioClip riverSound;

    public AudioSource output;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySound(string type)
    {
        if(type == "River" && riverSound != null)
        {
            output.clip = riverSound;
            output.Play();
        }
        else
        {
            output.clip = defaultClip;
            output.Play();
        }
    }
}
