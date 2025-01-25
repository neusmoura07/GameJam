using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject baseProjectile;

    [Space]

    [SerializeField] private bool hasCharge;
    [SerializeField] private GameObject chargedProjectile;
    [SerializeField] private float timeToCharge;
    [SerializeField] private ParticleSystem chargeParticle;

    private float chargeTime;



    void Start()
    {
    }

    void Update()
    {
        
        // Verifica se o player não está morto antes de atirar
        if (!player.Instance.isDead)
        {
            HandleShooting();
        }
        
        
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire(baseProjectile);
        }

        if (!hasCharge) return;

        if (Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime;
        }

        if (chargeTime >= timeToCharge)
        {
            if (!chargeParticle.isPlaying) chargeParticle.Play();
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (chargeTime >= timeToCharge)
            {
                Fire(chargedProjectile);
            }

            chargeTime = 0f;
            if (chargeParticle.isPlaying) chargeParticle.Stop();
        }
    }

    private void Fire(GameObject projectile)
    {
        // Instantiate o projetil
        var newProjectile = Instantiate(projectile, transform.position, transform.rotation);

        // Obtém a direção do player (como um int)
        int playerDirection = (int)Mathf.Sign(player.Instance.direction.x);

        // Envia a direção como inteiro para o projetil
        newProjectile.GetComponent<Projectile>().SetDirection(playerDirection);

        // Animação de tiro
        // PlayerSc
    }
}
