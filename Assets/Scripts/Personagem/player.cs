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


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
    void Jump()
    {
        if (Input.GetButtonDown("Jump") )
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
}
