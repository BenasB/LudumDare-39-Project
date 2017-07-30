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
        gm.ResetMaterialColor();
        SceneManager.LoadScene("Menu");
    }

    public void LoadNextLevel()
    {
        gm.ResetMaterialColor();
        SceneManager.LoadScene("Level " + PlayerPrefs.GetInt("Level"));
    }

    public void RetryLevel()
    {
        gm.ResetMaterialColor();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
