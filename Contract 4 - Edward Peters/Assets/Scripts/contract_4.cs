//EP231933
//MIT License

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Threading;

public class contract_4 : MonoBehaviour
{
    // Initialising Audio variables that will be used later
    private AudioSource audioSource;
    private AudioClip outAudioClip;

    // Boolean that will be used to ensure parameters aren't reset to default upon changing scenes
    public static bool started = false;

    // Initialising variables to do with the length of audio played
    public float defaultTime;
    public static float clickTime { get; set; }
    public static float mouseOverTime { get; set; }
    public Slider timeSlider;

    // Initialising variables to do with the quality of audio played
    public int defaultSampleRate;
    public static int clickSampleRate { get; set; }
    public static int mouseOverSampleRate { get; set; }
    public Slider sampleRateSlider;

    // Initialising variables to do with the pitch of audio played
    public int defaultFrequency;
    public static int clickFrequency { get; set; }
    public static int mouseOverFrequency { get; set; }
    public Slider frequencySlider;


    // Private function that generates a tone based on parameters chosen by the user
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

    void Start()
    {
        // Before the first frame update the Audio Source is defined
        audioSource = GetComponent<AudioSource>();
        // The previously mentioned boolean will make sure this code - which sets all parameters to their default values - will only run on the initial startup
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
        // These if statements set the sliders to the correct positions upon opening a scene
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
        // These if statements define which tone is currently being edited - based on which scene is currently open
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

    // Function that pauses the program as the click tone is played, before the scene is changed (or the build is closed)
    public void ChangeScene()
    {
        PlayOutClickAudio();
        Thread.Sleep((int)Math.Round(1000 * clickTime));
    }

    // Function that opens the click tone editor scene
    public void StartClickEditor()
    {
        ChangeScene();
        SceneManager.LoadScene(1);
    }

    // Function that opens the mouse over tone editor scene
    public void StartMouseOverEditor()
    {
        ChangeScene();
        SceneManager.LoadScene(2);
    }

    // Function that plays the click tone
    public void PlayOutClickAudio()
    {
        outAudioClip = CreateToneAudioClip(clickFrequency, clickSampleRate);
        audioSource.PlayOneShot(outAudioClip);
    }

    // Function that plays the mouse over tone
    public void PlayOutMouseOverAudio()
    {
        outAudioClip = CreateToneAudioClip(mouseOverFrequency, mouseOverSampleRate);
        audioSource.PlayOneShot(outAudioClip);
    }

    // Function that closes the build
    public void QuitGame()
    {
        ChangeScene();
        Application.Quit();
    }

    // Function that re-opens the starting menu scene
    public void Back()
    {
        ChangeScene();
        SceneManager.LoadScene(0);
    }
}
