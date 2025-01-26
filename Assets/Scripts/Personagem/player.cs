using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{
    public static player Instance { get; private set; } // Singleton
    public Vector2 direction { get; private set; } // Direção do player
    public float Speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;
    private Rigidbody2D rig;
    private Animator anim;

    public bool isDead;

    bool isBlowing;

    public int maxHealth = 120;
    public int currentHealth;

    public GameObject balaProjetil;
    public Transform arma;
    private bool tiro;
    public float forcaDoTiro;

    // Novos campos para áudio
    public AudioClip deathSound; // Som de morte
    private AudioSource audioSource;
    

    void Awake()
    {
        // Implementação do Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Garante que apenas uma instância exista
            return;
        }
        Instance = this;
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        direction = Vector2.right; // Direção inicial

        // Configuração do AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if(!isDead)
        {
            Move();
            Jump();
            Attack();
        }
        
        
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        if (Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true); // Ativa a animação
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            direction = Vector2.right; // Direção para a direita

        }
        else if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true); // Ativa a animação
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            direction = Vector2.left; // Direção para a esquerda
        }

        if (Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("walk", false); // Ativa a animação
            

        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isBlowing )
        {
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if(doubleJump)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Chao"))
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

    }

    

    void OnCollisionExit2D(Collision2D collision) 
    {
       if(collision.gameObject.CompareTag("Chao"))
        {
            isJumping = true;
        
        } 
    }

    void Attack()
    {
        if(!PauseMenu.isPaused)
        {
            if (Input.GetMouseButtonDown(0)) // Verifica se o botão esquerdo do mouse foi pressionado
        {
            anim.SetTrigger("attack"); // Ativa a animação de ataque
        }
        }
        

        
    } 

    void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Fan")
        {
            isBlowing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "Fan")
        {
            isBlowing = false;
        }
    }

    public void Die()
    {
        if (!isDead)
            {
                isDead = true;
                anim.SetTrigger("dead");

                // Pausar a música de fundo
                if (BackgroundMusic.Instance != null)
                {
                    BackgroundMusic.Instance.PauseMusic();
                }

                // Tocar o som de morte
                if (deathSound != null)
                {
                    audioSource.PlayOneShot(deathSound);
                }

                StartCoroutine(ShowGameOverAfterDeath());
            }
    }

    IEnumerator ShowGameOverAfterDeath()
    {
        yield return new WaitForSeconds(1f); // Wait for the death animation to finish
        GameController.instance.ShowGameOver();
    }
}

