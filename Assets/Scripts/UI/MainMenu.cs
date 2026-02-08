using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void LoadEasyBot()
    {
        SceneManager.LoadScene("EasyBot");
    }
    public void LoadHardBot()
    {
        SceneManager.LoadScene("HardBot");
    }

    public void Load1v1()
    {
        SceneManager.LoadScene("1v1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
