using UnityEngine;
using UnityEngine.UI;
using TMPro; // Adicionar este using

public class ScoreControl : MonoBehaviour
{

    public int totalScore;
    public TextMeshProUGUI scoreText;

    public static ScoreControl instance;

    public GameObject objetodestruido;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }
    
    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();

        if (totalScore >= 3 && objetodestruido != null)
        {
            Destroy(objetodestruido); // Destroi o objeto
        }
    }
   
}
