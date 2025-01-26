using UnityEngine;

public class BulletScript : MonoBehaviour
{

    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Personagem");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.linearVelocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject,2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
