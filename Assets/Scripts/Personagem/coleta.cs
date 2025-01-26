using UnityEngine;

public class coleta : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;
    public GameObject collected;

    public int score;

    // Campo para o áudio de coleta
    public AudioClip collectSound;  // O áudio que será tocado
    private AudioSource audioSource; // Referência para o AudioSource

    
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();

        // Verifica se o AudioSource já está presente no objeto
        audioSource = GetComponent<AudioSource>();

        // Caso não tenha, adiciona um novo AudioSource ao objeto
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Personagem")
        {

            // Toca o som de coleta se o AudioClip foi atribuído
            if (collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);  // Toca o som uma vez
            }
            
            sr.enabled = false;
            circle.enabled = false;
            collected.SetActive(true);

            ScoreControl.instance.totalScore += score;
            ScoreControl.instance.UpdateScoreText();

            Destroy(gameObject, 0.35f);
        }
    }
}
