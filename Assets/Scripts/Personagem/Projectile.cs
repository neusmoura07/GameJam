using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;

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
    
}
