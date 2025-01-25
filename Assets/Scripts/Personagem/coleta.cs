using UnityEngine;

public class coleta : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;
    public GameObject collected;

    public int score;

    
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Personagem")
        {
            sr.enabled = false;
            circle.enabled = false;
            collected.SetActive(true);

            ScoreControl.instance.totalScore += score;
            ScoreControl.instance.UpdateScoreText();

            Destroy(gameObject, 0.35f);
        }
    }
}
