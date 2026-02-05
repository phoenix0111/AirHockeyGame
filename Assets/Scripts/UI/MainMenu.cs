using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        Screen.SetResolution(1080, 2400, true);

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
