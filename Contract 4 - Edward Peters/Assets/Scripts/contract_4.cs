using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Threading;

public class contract_4 : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    public static bool started = false;

    public float defaultTime;
    public static float clickTime { get; set; }
    public static float mouseOverTime { get; set; }
    public Slider timeSlider;

    public int defaultSampleRate;
    public static int clickSampleRate { get; set; }
    public static int mouseOverSampleRate { get; set; }
    public Slider sampleRateSlider;

    public int defaultFrequency;
    public static int clickFrequency { get; set; }
    public static int mouseOverFrequency { get; set; }
    public Slider frequencySlider;


    // Private
    private AudioClip CreateToneAudioClip(int clickFrequency, int clickSampleRate)
    {
        int sampleLength = (int)Math.Round(clickSampleRate * clickTime);
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("tone", sampleLength, 1, clickSampleRate, false);

        float[] samples = new float[sampleLength];
        for (var i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * clickFrequency * ((float)i / (float)clickSampleRate));
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
        if (started == false)
        {
            clickTime = defaultTime;
            clickSampleRate = defaultSampleRate;
            clickFrequency = defaultFrequency;
            mouseOverTime = defaultTime;
            mouseOverSampleRate = defaultSampleRate;
            mouseOverFrequency = defaultFrequency;
            started = true;
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            timeSlider.value = clickTime;
            sampleRateSlider.value = clickSampleRate;
            frequencySlider.value = clickFrequency;
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            timeSlider.value = mouseOverTime;
            sampleRateSlider.value = mouseOverSampleRate;
            frequencySlider.value = mouseOverFrequency;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            clickTime = timeSlider.value;
            clickSampleRate = (int)Math.Round(sampleRateSlider.value);
            clickFrequency = (int)Math.Round(frequencySlider.value);
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            mouseOverTime = timeSlider.value;
            mouseOverSampleRate = (int)Math.Round(sampleRateSlider.value);
            mouseOverFrequency = (int)Math.Round(frequencySlider.value);
        }
    }

    public void StartClickEditor()
    {
        ChangeScene();
        SceneManager.LoadScene(1);
    }

    public void StartMouseOverEditor()
    {
        ChangeScene();
        SceneManager.LoadScene(2);
    }

    public void PlayOutClickAudio()
    {
        outAudioClip = CreateToneAudioClip(clickFrequency, clickSampleRate);
        audioSource.PlayOneShot(outAudioClip);
    }

    public void PlayOutMouseOverAudio()
    {
        outAudioClip = CreateToneAudioClip(mouseOverFrequency, mouseOverSampleRate);
        audioSource.PlayOneShot(outAudioClip);
    }

    public void QuitGame()
    {
        ChangeScene();
        Application.Quit();
    }

    public void Back()
    {
        ChangeScene();
        SceneManager.LoadScene(0);
    }

    public void ChangeScene()
    {
        PlayOutClickAudio();
        Thread.Sleep((int)Math.Round(1000 * clickTime));
    }
}
