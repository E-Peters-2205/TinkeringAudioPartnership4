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
    
    int Duration = 1;


    // the frquancy of notes
    int Frequancy = 1500;

    int Note_C = (1047);
    int Note_D = (1175);


    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        //outAudioClip = CreateToneAudioClip(Frequancy, Duration);

        CreateMelody();

    }

    void Update()
    {
        //PlayOutAudio();
    }    

    // Public APIs
    public void PlayOutAudio() {
        audioSource.PlayOneShot(outAudioClip);    
    }


    public void StopAudio() {
        audioSource.Stop();
    }
    
    
    // Private 
    private AudioClip CreateToneAudioClip(int Frequency, int Duration) {
        int sampleDurationSecs = Duration;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;              //Above sets variables that will be used to create the tone.
        float maxValue = 1f / 4f;                                        // max value is a multiplyer to evenly change every audio sample. 
        
        var audioToneClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);
        
        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++) {                                                     //for loop to go through all samples in the tone and create their audio number equivilent.
            float s = Mathf.Sin(2.0f * Mathf.PI * Frequency * ((float) i / (float) sampleRate));     // takes all the input data and creates the sound number for that sample
            float v = s * maxValue;                                                                  // applys the multiplyer to the sample
            samples[i] = v;                                                                          // an array of all the audio number in the order they are played.
        }

        audioToneClip.SetData(samples, 0);         //telling the samples that they represent audio numbers.
        return (audioToneClip); 
    }

    // Below sets simple melody.

    private void CreateMelody() {


        List<AudioClip> Audio_Clips = new List<AudioClip>();    //makes an empty list to add future clips too
        
        int count = 0;                                          // a counter for the loop

        System.Random r = new System.Random(); 

        while (count < 10)                                      // a loop to create different notes
        {
            int freq = 440;                                     // creates a variable to store temopary freqancy

            if(r.Next(10) % 2 == 0)                             // number checker to see if odd or even, this changes the note that is played  
            {
                freq = Note_C;                           
            }
            else
            {
                freq = Note_D;
            }

            Audio_Clips.Add(CreateToneAudioClip(freq, Duration));                //gets the audio numbers for first note in this case C and stores it in an array 
            //Audio_Clips.Add(CreateToneAudioClip(Note_D,Duration));                //gets audio numbers for second Note 
                                                                                  //Audio_Clips.Add(CreateToneAudioClip(220*count, Duration));  
            count++;
        }

        int targetSamples = 0;                   // sets a variable to 0 to count the samples

        foreach (AudioClip note in Audio_Clips)  // this loop counts the samples 
        {
            targetSamples += note.samples;           
        }

        

        AudioClip melodyClip = AudioClip.Create("melody", targetSamples, 1, 44100, false);  // creates a melody from all the samples

        int offsetPosition = 0;                                     //this variable is used to mark the start of an audio clip 

        foreach (AudioClip note in Audio_Clips)                     // a loop to add the audio clip to melody clip in order
        {
            

            float[] samples = new float[note.samples];
            note.GetData(samples, 0);

            melodyClip.SetData(samples, offsetPosition);

            offsetPosition += note.samples;
        }

        audioSource.PlayOneShot(melodyClip);


        // need to save the audio file 

        /*
        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        for (var c = 0; c < Melody_Chunks.Length; c++) {       // seting up a loop to add all the tones together ino a single audio clip 
            
            audioClip.SetData(Melody_Chunks[c], c);              

        }
        */
    }

}