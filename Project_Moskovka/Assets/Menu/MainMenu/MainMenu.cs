using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game_World");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
