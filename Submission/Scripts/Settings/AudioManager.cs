using UnityEngine;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MusicPref = "MusicPref";
    private static readonly string SFXPref = "SFXPref";
    private int firstPlayInt;
    public Slider musicSlider, soundFXSlider;
    private float musicFloat, soundFXfloat;

    public AudioSource musicAudio;
    public AudioSource[] soundFXAudio;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        /*if (firstPlayInt == 0)
        {*/
            musicFloat = .80f;
            soundFXfloat = .80f;
            musicSlider.value = musicFloat;
            soundFXSlider.value = soundFXfloat;

            PlayerPrefs.SetFloat(MusicPref, musicFloat);
            PlayerPrefs.SetFloat(SFXPref, soundFXfloat);

            PlayerPrefs.SetInt(FirstPlay, -1);
        /*}
        else
        {
            musicFloat = PlayerPrefs.GetFloat(MusicPref);
            musicSlider.value = musicFloat;
            soundFXfloat = PlayerPrefs.GetFloat(SFXPref);
            soundFXSlider.value = soundFXfloat;
        }*/
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MusicPref, musicSlider.value);
        PlayerPrefs.SetFloat(SFXPref, soundFXSlider.value);
    }

    // This saves the values even after exiting the game
    void OnAppFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        musicAudio.volume = musicSlider.value;

        for(int i = 0; i < soundFXAudio.Length; i++)
        {
            soundFXAudio[i].volume = soundFXSlider.value;
        }
    }
}
