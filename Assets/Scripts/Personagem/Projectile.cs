using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int projectileDamage;

    private Rigidbody2D projectileRb;
    private int direction;
    private bool hasDirection;
    void Start()
    {
        Destroy(gameObject, lifeTime);
        projectileRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasDirection)
        {
            projectileRb.linearVelocity = new Vector2(direction * projectileSpeed, 0f);
        }
    }

    public void SetDirection(int dir)
    {
        hasDirection = true;
        direction = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se colidiu com um inimigo
        var enemy = collision.collider.GetComponent<PatrulEnemyController>();
        var pursuingEnemy = collision.collider.GetComponent<PursuingPlayerController>();
        var bossenemy = collision.collider.GetComponent<enemyFollowPlayer>();

        if (enemy != null)
        {
            // Aplica dano ao inimigo patrulha
            enemy.TakeDamage(projectileDamage);
            Destroy(gameObject); // Destroi o projétil após o impacto
        }
        else if (pursuingEnemy != null)
        {
            // Aplica dano ao inimigo perseguidor
            pursuingEnemy.TakeDamage(projectileDamage);
            Destroy(gameObject); // Destroi o projétil após o impacto
        }

        else if (bossenemy != null)
        {
            // Aplica dano ao inimigo perseguidor
            bossenemy.TakeDamage(projectileDamage);
            Destroy(gameObject); // Destroi o projétil após o impacto
        }
    }

}
