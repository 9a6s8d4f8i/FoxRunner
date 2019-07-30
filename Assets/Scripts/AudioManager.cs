using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource audioEffects;
    public static AudioManager inst= null;

    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else if( inst != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudio(AudioClip effect)
    {
        audioEffects.clip = effect;
        audioEffects.Play();
    }
}
