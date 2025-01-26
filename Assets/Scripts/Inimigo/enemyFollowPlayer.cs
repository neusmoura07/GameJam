using UnityEngine;
using System.Collections;

public class enemyFollowPlayer : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject bullet;
    public GameObject bullentParent;
    private Transform player;

    // Sistema de vida
    public int maxHealth = 5; // Vida máxima do inimigo
    private int currentHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Personagem").transform;
        currentHealth = maxHealth; // Define a vida inicial como o valor máximo
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(bullet, bullentParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

    // Método para receber dano
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die(); // Chama o método Die() ao atingir 0 de vida
        }
    }

    // Método para destruir o inimigo
    private void Die()
    {
        Destroy(gameObject); // Remove o inimigo da cena
    }

    // Desenha as áreas de detecção no editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
