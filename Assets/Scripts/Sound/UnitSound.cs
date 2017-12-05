using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSound : MonoBehaviour {
    public AudioClip defaultClip;
    public AudioClip grassSound;
    public AudioClip roadSound;
    public AudioClip mountainSound;
    public AudioClip riverSound;
    public AudioClip forestSound;
    public AudioClip buildingSound;
	public AudioClip attackSound;

    public AudioSource output;
    AudioSettings settings;
    public float volume;

    private void Awake()
    {
        volume = PlayerPrefs.GetFloat("st");
    }
    // Use this for initialization
    void Start () {
        //settings = GameObject.FindGameObjectWithTag("SoundSettings").GetComponent<AudioSettings>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySound(string type)
    {
        output.volume = volume;
         if (type == "River" && riverSound != null)
         {
            output.clip = riverSound;
            output.Play();
         }
            else if (type == "Grass" && grassSound != null)
            {
                output.clip = grassSound;
                output.Play();
            }
            else if(type == "Road" && roadSound != null)
            {
                output.clip = roadSound;
                output.Play();
            }
            else if (type == "Mountain" && mountainSound != null)
            {
                output.clip = mountainSound;
                output.Play();
            }
        else if (type == "Forest" && forestSound != null)
        {
            output.clip = forestSound;
            output.Play();
        }
        else if (type == "Building" && buildingSound != null)
        {
            output.clip = buildingSound;
            output.Play();
        }
        else
            {
                output.clip = defaultClip;
                output.Play();
            }
        }
}
