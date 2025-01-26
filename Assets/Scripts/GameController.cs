using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOver;

    public static GameController instance;
    
    void Start()
    {
        instance = this;
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void RestartGame(string lvlname)
    {
        // Retomar a música de fundo
        if (BackgroundMusic.Instance != null)
        {
            BackgroundMusic.Instance.ResumeMusic();
        }

        // Recarregar a cena
        SceneManager.LoadScene(lvlname);
    }
}
