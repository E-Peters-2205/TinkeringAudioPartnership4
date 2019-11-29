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
    private object SavWav;



    // Variables to change notes ect

    double Duration = 1;


    // the frquancy of notes


    

    List<int> TonesC6_B6 = new List<int>() { 1047, 1175, 1319, 1379, 1568, 1760, 1976}; 



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
    private AudioClip CreateToneAudioClip(int Frequency, double Duration) {                   
        
        int sampleRate = 44100;
        int sampleLength =  Convert.ToInt32 (Math.Floor(sampleRate * Duration));              //Above sets variables that will be used to create the tone.
        float maxValue = 1f / 4f;                                                             // max value is a multiplyer to evenly change every audio sample. 
        
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
        
        

        // for loop to keep trak of notes going up and back
        Duration = 1;
        for (int Tone = 0; Tone < TonesC6_B6.Count; Tone++){                //A for loop for the notes as they climb

            int freq = TonesC6_B6[Tone];
            Audio_Clips.Add(CreateToneAudioClip(freq, Duration));
        }

        for (int Note = TonesC6_B6.Count - 1; Note > -1; Note--){                    //A for loop for the notes falling -1 to compensate for the count starting from 1

            int freq = TonesC6_B6[Note];
            Audio_Clips.Add(CreateToneAudioClip(freq, Duration));
        }
        
        
        int count = 0;                                                       // a counter for the loop, Duration set for this section of melody
        System.Random random_number = new System.Random();                   // new random variable
        Duration = 0.5;                                                      // changes the time each note will play for
        while (count < 10)                                                   
        { 
            int R = random_number.Next(0, TonesC6_B6.Count);                    // sets limits to random number(min/max). no need for -1 here as random dosent include last number. 

            int freq = TonesC6_B6[R];                                           // sets R to a random number between 0 and the number of notes

            Audio_Clips.Add(CreateToneAudioClip(freq, Duration));
            Audio_Clips.Add(CreateToneAudioClip(freq - 100, Duration));
            Audio_Clips.Add(CreateToneAudioClip(freq - 200, Duration));         
            Audio_Clips.Add(CreateToneAudioClip(freq - 300, Duration));
            Audio_Clips.Add(CreateToneAudioClip(freq - 400, Duration));
            Audio_Clips.Add(CreateToneAudioClip(freq , Duration));              // added a few more frequenceys to give sets of three a gradually deeper tone and end with the same freqency

            count++;

        }
            
            
        int targetSamples = 0;                   // sets a variable to 0 to count the samples

        foreach (AudioClip note in Audio_Clips)  // this loop counts the samples 
        {
            targetSamples += note.samples;       // adds the amout of samples in each clip, usefull for when the duration of samples changes.    
        }

        

        AudioClip melodyClip = AudioClip.Create("melody", targetSamples, 1, 44100, false);  // creates a melody from all the samples

        int offsetPosition = 0;                                                             //this variable is used to mark the start of an audio clip 

        foreach (AudioClip note in Audio_Clips)                                             // a loop to add the audio clip to melody clip in order
        {
            

            float[] samples = new float[note.samples];              
            note.GetData(samples, 0);

            melodyClip.SetData(samples, offsetPosition);          //adds current sample to melody clip at current offset

            offsetPosition += note.samples;                       // updates the offseet for the next tone, the next tone starts as the last tone ends
        }

        audioSource.PlayOneShot(melodyClip);

        

    }

}

