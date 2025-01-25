using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
    {
    public float Speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;
    private Rigidbody2D rig;
    private Animator anim;

    public bool isDead;

    bool isBlowing;

    public int maxHealth = 120; // Vida máxima do personagem
    public int currentHealth;  // Vida atual do personagem


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth; // Inicializa a vida atual com a vida máxima
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
        Jump();
        Attack();
    }

    void Move()
    {
        if(!isDead)
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * Speed;

            if(Input.GetAxis("Horizontal") > 0f)
            {
                anim.SetBool("walk", true);
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }

            if(Input.GetAxis("Horizontal") < 0f)
            {
                anim.SetBool("walk", true);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }

            if(Input.GetAxis("Horizontal") == 0f)
            {
                anim.SetBool("walk", false);
            }
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
                StartCoroutine(ShowGameOverAfterDeath());
            }
    }

    IEnumerator ShowGameOverAfterDeath()
    {
        yield return new WaitForSeconds(2.5f); // Wait for the death animation to finish
        GameController.instance.ShowGameOver();
    }
}

