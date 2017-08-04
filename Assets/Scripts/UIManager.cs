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
        SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("Level"));
        if (PlayerPrefs.GetInt("Level") != 11)
            SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
    }

    public void RetryLevel()
    {
        gm.ResetMaterialColor();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
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
