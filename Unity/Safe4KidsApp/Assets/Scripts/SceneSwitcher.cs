using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GotoHardScene()
    {
        SceneManager.LoadScene("Hard");
    }

    public void GotoEasyScene()
    {
        SceneManager.LoadScene("Easy");
    }
    public void GotoMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
