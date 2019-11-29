using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using System.Threading;

public class contract_4 : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip outAudioClip;
    public int frequency;
    public int sampleRate;
    public float time;

    // Private
    private AudioClip CreateToneAudioClip(int frequency, int sampleRate)
    {
        int sampleLength = (int)Math.Round(sampleRate * time);
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
        outAudioClip = CreateToneAudioClip(frequency, sampleRate);
    }

    public void StartEditor()
    {
        PlayOutAudio();
        Thread.Sleep((int)Math.Round(1000 * time));
        SceneManager.LoadScene(1);
    }

    public void PlayOutAudio()
    {
        audioSource.PlayOneShot(outAudioClip);
    }

    public void QuitGame()
    {
        PlayOutAudio();
        Thread.Sleep((int)Math.Round(1000 * time));
        Application.Quit();
    }

    public void Back()
    {
        PlayOutAudio();
        Thread.Sleep((int)Math.Round(1000 * time));
        SceneManager.LoadScene(0);
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
