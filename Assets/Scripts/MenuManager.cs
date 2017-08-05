using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Image MusicImage;
    public Image SFXImage;
    public Sprite[] Music;
    public Sprite[] SFX;

    private MusicPlayer mp;
    private int musicCount;
    private int sfxCount;

    private void Start()
    {
        mp = MusicPlayer.Instance;
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 1);
        }

        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetFloat("Music", 0.6f);
            musicCount = 2;
            MusicImage.sprite = Music[2];
            mp.ChangeVolume(0.6f);
        }
        else
        {
            if (PlayerPrefs.GetFloat("Music") == 1f)
                musicCount = 3;
            else if (PlayerPrefs.GetFloat("Music") == 0.6f)
                musicCount = 2;
            else if (PlayerPrefs.GetFloat("Music") == 0.3f)
                musicCount = 1;
            else if (PlayerPrefs.GetFloat("Music") == 0f)
                musicCount = 0;
            MusicImage.sprite = Music[musicCount];
            mp.ChangeVolume(PlayerPrefs.GetFloat("Music")); 
        }

        if (!PlayerPrefs.HasKey("SFX"))
        {
            PlayerPrefs.SetFloat("SFX", 0.6f);
            sfxCount = 2;
            SFXImage.sprite = SFX[2];
        }
        else
        {
            if (PlayerPrefs.GetFloat("SFX") == 1f)
                sfxCount = 3;
            else if (PlayerPrefs.GetFloat("SFX") == 0.6f)
                sfxCount = 2;
            else if (PlayerPrefs.GetFloat("SFX") == 0.3f)
                sfxCount = 1;
            else if (PlayerPrefs.GetFloat("SFX") == 0f)
                sfxCount = 0;
            SFXImage.sprite = SFX[sfxCount];
        }
    }

    public void StartGame()
    {
        if (SceneListCheck.Has("Level " + PlayerPrefs.GetInt("Level")))
        {
            SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("Level"));
            SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene("Credits");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void AddMusic()
    {
        if (musicCount + 1 != 4)
            musicCount++;
        else
            musicCount = 0;

        MusicImage.sprite = Music[musicCount];
        if (musicCount == 0)
            PlayerPrefs.SetFloat("Music", 0);
        else if (musicCount == 1)
            PlayerPrefs.SetFloat("Music", 0.3f);
        else if (musicCount == 2)
            PlayerPrefs.SetFloat("Music", 0.6f);
        else if (musicCount == 3)
            PlayerPrefs.SetFloat("Music", 1f);

        mp.ChangeVolume(PlayerPrefs.GetFloat("Music"));
    }

    public void AddSFX()
    {
        if (sfxCount + 1 != 4)
            sfxCount++;
        else
            sfxCount = 0;

        SFXImage.sprite = SFX[sfxCount];
        if (sfxCount == 0)
            PlayerPrefs.SetFloat("SFX", 0);
        else if (sfxCount == 1)
            PlayerPrefs.SetFloat("SFX", 0.3f);
        else if (sfxCount == 2)
            PlayerPrefs.SetFloat("SFX", 0.6f);
        else if (sfxCount == 3)
            PlayerPrefs.SetFloat("SFX", 1f);
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("Level", 1);
    }
}
