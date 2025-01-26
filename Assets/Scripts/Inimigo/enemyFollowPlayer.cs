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

    private Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Personagem").transform;
        currentHealth = maxHealth; // Define a vida inicial como o valor máximo
        anim = GetComponent<Animator>();
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
        anim.SetTrigger("dead");
        StartCoroutine(WaitForDeathAnimation()); // Espera a animação de morte ser concluída
    }

    // Desenha as áreas de detecção no editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
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
