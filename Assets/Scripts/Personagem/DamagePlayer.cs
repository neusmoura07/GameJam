using UnityEngine;
using System;
using System.Collections;

public class DamagePlayer : MonoBehaviour
{

    public float KnockBackForce = 30f;
    public float intagibleTime = 1f;
    public SpriteRenderer sprite;
    private int originalLayer;
    public Rigidbody2D rb;
    public LifeController lifeController;
    
    void Start()
    {
        lifeController = GetComponent<LifeController>();
        rb = GetComponent<Rigidbody2D>();
        originalLayer = gameObject.layer;
        sprite = GetComponent<SpriteRenderer>();
    }

   private void OnCollisionEnter2D(Collision2D collision)
   {
    if(collision.gameObject.tag == "Enemy")
    {
        TakeDamage(collision);
    }
   
   }

   private void TakeDamage(Collision2D collision)
   {
    StartCoroutine(BecomeIntagible());
    ApplyKnockBack(collision);
    lifeController.DropVidas();
   }

   private void ApplyKnockBack(Collision2D collision)
   {
    Vector2 directionKnockBack = (transform.position - collision.transform.position).normalized;
    rb.AddForce(directionKnockBack * KnockBackForce, ForceMode2D.Impulse);
   }

   private IEnumerator BecomeIntagible()
   {
    gameObject.layer = LayerMask.NameToLayer("Intangible");
    Color color = sprite.color;
    color.a = 0.5f;
    sprite.color = color;
    yield return new WaitForSeconds(intagibleTime);
    gameObject.layer = originalLayer;
    color.a = 1f;
    sprite.color = color;
   }
}
