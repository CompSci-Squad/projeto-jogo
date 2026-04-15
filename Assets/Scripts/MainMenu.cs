using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1); // Fase 1
    }

    public void QuitGame()
    {
        Debug.Log("Saiu do jogo");

        Application.Quit();
    }
}