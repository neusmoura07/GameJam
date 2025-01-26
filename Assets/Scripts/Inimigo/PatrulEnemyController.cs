using UnityEngine;
using System.Collections;
public class PatrulEnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 4;

    private PursuingPlayerController pursuingPlayerController;

    public int maxHealth = 3; // Vida máxima do inimigo
    private int currentHealth;

    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth; // Inicializa a vida atual
        pursuingPlayerController = GetComponent<PursuingPlayerController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        anim.SetTrigger("dead");
        StartCoroutine(WaitForDeathAnimation()); // Espera a animação de morte ser concluída
    }

    private IEnumerator WaitForDeathAnimation()
    {
        // Obtém a duração da animação de morte a partir do Animator
        float deathAnimationTime = anim.GetCurrentAnimatorStateInfo(0).length;

        // Aguarda a animação de morte terminar
        yield return new WaitForSeconds(deathAnimationTime);

        // Após a animação, desativa o inimigo
        gameObject.SetActive(false);
    }
        
}
