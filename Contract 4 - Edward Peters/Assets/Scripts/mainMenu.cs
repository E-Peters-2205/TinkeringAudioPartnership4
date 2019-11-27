using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class mainMenu : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    public int frequency2;
    public int time;

    // Private 
    private AudioClip CreateToneAudioClip(int frequency)
    {
        int sampleRate = 44100;
        int sampleLength = sampleRate * time;
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        outAudioClip = CreateToneAudioClip(frequency2);
    }

    public void StartGame()
    {
        PlayOutAudio();
    }

    public void PlayOutAudio()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    public void QuitGame()
    {
        PlayOutAudio();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
