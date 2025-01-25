using UnityEngine;
using UnityEngine.UI; // Necessário para usar UI
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject baseProjectile;

    [Header("Charge Settings")]
    [SerializeField] private bool hasCharge;
    [SerializeField] private GameObject chargedProjectile;
    [SerializeField] private float timeToCharge = 1f;
    [SerializeField] private ParticleSystem chargeParticle;

    [Header("Cooldown Settings")]
    [SerializeField] private float burstCooldown = 0.8f;
    [SerializeField] private float chargedCooldown = 2f;

    [Header("Burst Settings")]
    [SerializeField] private int burstCount = 3;
    [SerializeField] private float burstDelay = 0.1f;

    [Header("UI Settings")]
    [SerializeField] private Slider burstCooldownSlider; // Referência ao slider da rajada
    [SerializeField] private Slider chargedCooldownSlider; // Referência ao slider do especial

    [Header("Animation Settings")]
    [SerializeField] private Animator playerAnimator; // Referência ao Animator do jogador

    private float burstCooldownTimer = 0f; // Temporizador da rajada
    private float chargedCooldownTimer = 0f; // Temporizador do especial
    private float chargeTime;

    private bool isFiringBurst = false;
    private bool isBurstOnCooldown = false;
    private bool isChargedOnCooldown = false;

    void Update()
    {
        if (!player.Instance.isDead)
        {
            HandleShooting();
        }

        UpdateCooldownUI();
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && !isBurstOnCooldown && !isFiringBurst)
        {
            if(!PauseMenu.isPaused)
            {
                StartCoroutine(FireBurst());
            }
        }

        if (!hasCharge) return;

        if (Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime;
        }

        if (chargeTime >= timeToCharge && !chargeParticle.isPlaying)
        {
            chargeParticle.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (chargeTime >= timeToCharge && !isChargedOnCooldown)
            {
                Fire(chargedProjectile);

                // Adiciona a animação de ataque ao disparar o chargedProjectile
                if (playerAnimator != null)
                {
                    playerAnimator.SetTrigger("attack"); // Assume que o parâmetro da animação é "Attack"
                }

                StartCoroutine(StartCooldown(chargedCooldown, isCharged: true));
            }

            chargeTime = 0f;
            if (chargeParticle.isPlaying) chargeParticle.Stop();
        }
    }

    private IEnumerator FireBurst()
    {
        isFiringBurst = true;

        for (int i = 0; i < burstCount; i++)
        {
            Fire(baseProjectile);
            yield return new WaitForSeconds(burstDelay);
        }

        isFiringBurst = false;

        StartCoroutine(StartCooldown(burstCooldown, isCharged: false));
    }

    private IEnumerator StartCooldown(float cooldownTime, bool isCharged)
    {
        if (isCharged)
        {
            isChargedOnCooldown = true;
            chargedCooldownTimer = cooldownTime;
        }
        else
        {
            isBurstOnCooldown = true;
            burstCooldownTimer = cooldownTime;
        }

        while (cooldownTime > 0f)
        {
            cooldownTime -= Time.deltaTime;

            if (isCharged)
                chargedCooldownTimer = cooldownTime;
            else
                burstCooldownTimer = cooldownTime;

            yield return null;
        }

        if (isCharged)
        {
            isChargedOnCooldown = false;
        }
        else
        {
            isBurstOnCooldown = false;
        }
    }

    private void UpdateCooldownUI()
    {
        if (burstCooldownSlider != null)
        {
            burstCooldownSlider.value = Mathf.Max(0, burstCooldown - burstCooldownTimer);
            burstCooldownSlider.maxValue = burstCooldown;
        }

        if (chargedCooldownSlider != null)
        {
            chargedCooldownSlider.value = Mathf.Max(0, chargedCooldown - chargedCooldownTimer);
            chargedCooldownSlider.maxValue = chargedCooldown;
        }
    }

    private void Fire(GameObject projectile)
    {   
        if(!PauseMenu.isPaused)
        {
            var newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            int playerDirection = (int)Mathf.Sign(player.Instance.direction.x);
            newProjectile.GetComponent<Projectile>().SetDirection(playerDirection);
        }
    }
}
