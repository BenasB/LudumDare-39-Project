using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Slider Music;
    public Slider SFX;

    private MusicPlayer mp;

    private void Start()
    {
        mp = MusicPlayer.Instance;
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetFloat("Music", 0.5f);
            Music.value = 0.5f;
            mp.ChangeVolume(Music.value);
        }
        else
        {
            Music.value = PlayerPrefs.GetFloat("Music");
            mp.ChangeVolume(Music.value); 
        }

        if (!PlayerPrefs.HasKey("SFX"))
        {
            PlayerPrefs.SetFloat("SFX", 0.5f);
            SFX.value = 0.5f;
        }
        else
        {
            SFX.value = PlayerPrefs.GetFloat("SFX");
        }
    }

    public void StartGame()
    {
        Debug.Log(PlayerPrefs.GetInt("Level"));
        SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("Level"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("Music", Music.value);
        mp.ChangeVolume(Music.value);
    }

    public void SaveSFXVolume()
    {
        PlayerPrefs.SetFloat("SFX", SFX.value);
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("Level", 1);
    }
}
