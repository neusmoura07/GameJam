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

    public Camera mainCamera;  // Referência para a câmera
    public float shakeDuration = 0.3f;  // Duração do tremor
    public float shakeMagnitude = 0.1f;  // Intensidade do tremor
    
    void Start()
    {
        lifeController = GetComponent<LifeController>();
        rb = GetComponent<Rigidbody2D>();
        originalLayer = gameObject.layer;
        sprite = GetComponent<SpriteRenderer>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Se não for atribuído, usa a câmera principal
        }
    }

   private void OnCollisionEnter2D(Collision2D collision)
   {
    if(collision.gameObject.tag == "Enemy")
    {
        TakeDamage(collision);
    }

    if(collision.gameObject.tag == "EnemyPlant")
    {
        TakeDamagePlant(collision);
    }
   
   }

   private void TakeDamage(Collision2D collision)
   {
    StartCoroutine(BecomeIntagible());
    ApplyKnockBack(collision);
    lifeController.DropVidas();
    StartCoroutine(ShakeScreen());  // Inicia o tremor da tela
   }

   private void TakeDamagePlant(Collision2D collision)
   {
    StartCoroutine(BecomeIntagible());
    ApplyKnockBack(collision);
    lifeController.DropVidasFull();
    StartCoroutine(ShakeScreen());  // Inicia o tremor da tela
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

   private IEnumerator ShakeScreen()
    {
        Vector3 originalPosition = mainCamera.transform.position;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            // Use UnityEngine.Random instead of Random
            float x = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);

            mainCamera.transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalPosition;  // Restaura a posição original da câmera
    }
}
