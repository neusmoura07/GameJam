using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreControl : MonoBehaviour
{
    public int totalScore; // Score total do jogo
    public TextMeshProUGUI scoreText; // Texto que exibe o score na tela
    public static ScoreControl instance; // Instância global para acesso fácil
    public GameObject objetodestruido; // Objeto que será destruído ao atingir certo score
    public GameObject winScreen; // Tela de vitória

    void Start()
    {
        instance = this; // Inicializa a instância para acesso global
    }
    
    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString(); // Atualiza o texto do score

        // Verifica e destrói o objeto se o score for maior ou igual a 3
        if (totalScore >= 3 && objetodestruido != null)
        {
            Destroy(objetodestruido);
        }

        // Verifica se o score ultrapassou 5
        if (totalScore > 5)
        {
            ActivateWinScreen();
        }
    }

    private void ActivateWinScreen()
    {
        // Ativa a tela de vitória
        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }

        // Congela o jogo
        Time.timeScale = 0f; // Congela o tempo no jogo
    }
}
