using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

//Credit Igor Aherne. Feel free to use as you wish, but mention me in credits :)
//www.facebook.com/igor.aherne

//audio source which holds a reference to Two audio sources, allowing to transition
//between incoming sound and the previously played one.
[ExecuteInEditMode]
public class DoubleAudioSource : MonoBehaviour
{

    AudioSource _source0;
    AudioSource _source1;


    #region internal vars
    bool _isFirst = true; //is _source0 currently the active AudioSource (plays some sound right now)

    Coroutine _zerothSourceFadeRoutine = null;
    Coroutine _firstSourceFadeRoutine = null;
    #endregion


    #region internal functionality
    void Reset()
    {
        Update();
    }


    void Awake()
    {
        Update();
    }



    void Update()
    {
        //constantly check if our game object doesn't contain audio sources which we are referencing.

        //if the _source0 or _source1 contain obsolete references (most likely 'null'), then
        //we will re-init them:
        if (_source0 == null || _source1 == null)
        {

            //re-connect _soruce0 and _source1 to the ones in attachedSources[]
            Component[] attachedSources = gameObject.GetComponents(typeof(AudioSource));
            //For some reason, unity doesn't accept "as AudioSource[]" casting. We would get
            //'null' array instead if we would attempt. Need to re-create a new array:
            AudioSource[] sources = attachedSources.Select(c => c as AudioSource).ToArray();

            InitSources(sources);

            return;
        }

    }


    //re-establishes references to audio sources on this game object:
    void InitSources(AudioSource[] audioSources)
    {

        if (ReferenceEquals(audioSources, null) || audioSources.Length == 0)
        {
            _source0 = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
            _source1 = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
            //DefaultTheSource(_source0);
            // DefaultTheSource(_source1);  //remove? we do this in editor only
            return;
        }

        switch (audioSources.Length)
        {
            case 1:
                {
                    _source0 = audioSources[0];
                    _source1 = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
                    //DefaultTheSource(_source1);  //TODO remove?  we do this in editor only
                }
                break;
            default:
                { //2 and more
                    _source0 = audioSources[0];
                    _source1 = audioSources[1];
                }
                break;
        }//end switch
    }

    #endregion




    //gradually shifts the sound comming from our audio sources to the this clip:
    // maxVolume should be in 0-to-1 range
    public void CrossFade(AudioClip playMe,
                           float maxVolume,
                           float fadingTime,
                           float delay_before_crossFade = 0)
    {

        var fadeRoutine = StartCoroutine(Fade(playMe,
                                                    maxVolume,
                                                    fadingTime,
                                                    delay_before_crossFade));
    }//end CrossFade()



    IEnumerator Fade(AudioClip playMe,
                      float maxVolume,
                      float fadingTime,
                      float delay_before_crossFade = 0)
    {


        if (delay_before_crossFade > 0)
        {
            yield return new WaitForSeconds(delay_before_crossFade);
        }

        if (_isFirst)
        { // _source0 is currently playing the most recent AudioClip
            //so launch on source1
            _source1.clip = playMe;
            _source1.Play();
            _source1.volume = 0;

            if (_firstSourceFadeRoutine != null)
            {
                StopCoroutine(_firstSourceFadeRoutine);
            }
            _firstSourceFadeRoutine = StartCoroutine(fadeSource(_source1,
                                                                _source1.volume,
                                                                maxVolume,
                                                                fadingTime));
            if (_zerothSourceFadeRoutine != null)
            {
                StopCoroutine(_zerothSourceFadeRoutine);
            }
            _zerothSourceFadeRoutine = StartCoroutine(fadeSource(_source0,
                                                                 _source0.volume,
                                                                 0,
                                                                 fadingTime));
            _isFirst = false;

            yield break;
        }

        //otherwise, _source1 is currently active, so play on _source0
        _source0.clip = playMe;
        _source0.Play();
        _source0.volume = 0;

        if (_zerothSourceFadeRoutine != null)
        {
            StopCoroutine(_zerothSourceFadeRoutine);
        }
        _zerothSourceFadeRoutine = StartCoroutine(fadeSource(_source0,
                                                            _source0.volume,
                                                            maxVolume,
                                                            fadingTime));

        if (_firstSourceFadeRoutine != null)
        {
            StopCoroutine(_firstSourceFadeRoutine);
        }
        _firstSourceFadeRoutine = StartCoroutine(fadeSource(_source1,
                                                            _source1.volume,
                                                            0,
                                                            fadingTime));
        _isFirst = true;
    }



    IEnumerator fadeSource(AudioSource sourceToFade, float startVolume, float endVolume, float duration)
    {
        float startTime = Time.time;

        while (true)
        {
            float elapsed = Time.time - startTime;

            sourceToFade.volume = Mathf.Clamp01(Mathf.Lerp(startVolume,
                                                            endVolume,
                                                            elapsed / duration));

            if (sourceToFade.volume == endVolume)
            {
                break;
            }
            yield return null;
        }//end while
    }


    //returns false if BOTH sources are not playing and there are no sounds are staged to be played.
    //also returns false if one of the sources is not yet initialized
    public bool isPlaying
    {
        get
        {
            if (_source0 == null || _source1 == null)
            {
                return false;
            }

            //otherwise, both sources are initialized. See if any is playing:
            if (_source0.isPlaying || _source1.isPlaying)
            {
                return true;
            }

            //none is playing:
            return false;
        }//end get
    }
    public void setVolume(float i)
    {
        _source0.volume = i;
        _source1.volume = i;
    }
}