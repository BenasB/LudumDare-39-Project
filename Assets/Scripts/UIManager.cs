using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    public void ExitToMenu()
    {
        if (gm)
            gm.ResetMaterialColor();
        SceneManager.LoadScene("Menu");
    }

    public void LoadNextLevel()
    {
        gm.ResetMaterialColor();
        if (SceneListCheck.Has("Level " + PlayerPrefs.GetInt("Level")))
        {
            SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("Level"));
            SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
        }else
        {
            SceneManager.LoadScene("Credits");
        }
    }

    public void RetryLevel()
    {
        gm.ResetMaterialColor();
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

    public void Pause()
    {
        gm.PauseGame();
    }

    public void Unpause()
    {
        gm.ReturnToGame();
    }
}
