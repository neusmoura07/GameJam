using UnityEngine;

public class PursuingPlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private Transform transformPlayer;
    public float detectionRadius = 5f;
    private bool facingRight = true;
    private PatrulEnemyController patrulEnemyController;

    public int maxHealth = 5;
    private int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
        patrulEnemyController = GetComponent<PatrulEnemyController>();
        rb = GetComponent<Rigidbody2D>();
        transformPlayer = GameObject.FindWithTag("Personagem").transform;
    }

    // Update is called once per frame
    void Update()
    {
        DetectionPlayer();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void DetectionPlayer()
    {
        float distanceToplayer = Vector2.Distance(transform.position,transformPlayer.position);

        if(distanceToplayer <= detectionRadius) 
        {
            FollowPLayer();
            patrulEnemyController.enabled = false;
            this.enabled = true;
        }
        else if (distanceToplayer > detectionRadius)
        {
            patrulEnemyController.enabled = true;
            this.enabled = false;
        }
    }

    private void FollowPLayer()
    {
        Vector2 direction = (transformPlayer.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * (speed + 2), rb.linearVelocity.y);
        PositionCorrection();
    }

    public void PositionCorrection()
    {
        if(rb.linearVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if(rb.linearVelocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    private void OnDrawGizmos()
     {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
