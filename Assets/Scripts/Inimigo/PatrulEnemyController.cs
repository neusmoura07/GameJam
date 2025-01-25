using UnityEngine;

public class PatrulEnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 4;

    private PursuingPlayerController pursuingPlayerController;

    public int maxHealth = 3; // Vida máxima do inimigo
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Inicializa a vida atual
        pursuingPlayerController = GetComponent<PursuingPlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (pursuingPlayerController != null)
        {
            pursuingPlayerController.DetectionPlayer();
        }

        Move();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        speed *= -1;
        if (pursuingPlayerController != null)
        {
            pursuingPlayerController.Flip();
        }
        else
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        if (pursuingPlayerController != null)
        {
            pursuingPlayerController.PositionCorrection();
        }
    }

    // Sistema de vida para o inimigo
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Desativa ou destrói o inimigo
        gameObject.SetActive(false); // Opcional: Substituir por Destroy(gameObject);
    }
}
