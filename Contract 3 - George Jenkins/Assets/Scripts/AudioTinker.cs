using System.Collections;
using System.Collections.Generic;
//using NaughtyAttributes;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class AudioTinker : MonoBehaviour {
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    // Variables to change notes ect
    int Frequancy = 1500;
    int Duration = 1;


    // sets notes

    int Note_C = (1047);
    int Note_D = (1175);
    
    
    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClip(Frequancy,Duration);
    }

    void Update()
    {
        PlayOutAudio();
    }    

    // Public APIs
    public void PlayOutAudio() {
        audioSource.PlayOneShot(outAudioClip);    
    }


    public void StopAudio() {
        audioSource.Stop();
    }
    
    
    // Private 
    private AudioClip CreateToneAudioClip(Frequency, Duration) {
        int sampleDurationSecs = Duration;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;              //Above sets variables that will be used to create the tone.
        float maxValue = 1f / 4f;                                        // max value is a multiplyer to evenly change every audio sample. 
        
        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);
        
        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++) {                                                     //for loop to go through all samples in the tone and create their audio number equivilent.
            float s = Mathf.Sin(2.0f * Mathf.PI * Frequency * ((float) i / (float) sampleRate));     // takes all the input data and creates the sound number for that sample
            float v = s * maxValue;                                                                  // applys the multiplyer to the sample
            samples[i] = v;                                                                          // an array of all the audio number in the order they are played.
        }

        audioClip.SetData(samples, 0);  //telling the samples that they represent adio numbers.
        return audioClip; 
    }

    // Below sets simple melody.

    private void CreateMelody() {

    }

}