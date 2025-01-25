using UnityEngine;

public class PatrulEnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 4;

    private PursuingPlayerController pursuingPlayerController;
    void Start()
    {
        pursuingPlayerController = GetComponent<PursuingPlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pursuingPlayerController != null)
        {
            pursuingPlayerController.DetectionPlayer();
        }
        
        move();
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        speed *= -1;
        if(pursuingPlayerController != null)
        {
           pursuingPlayerController.Flip(); 
        }
        else
        {
            flip();
        }
        
        
    }

    private void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }

    private void move()
    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        if(pursuingPlayerController != null)
        {
           pursuingPlayerController.PositionCorrection(); 
        }
        
    }
}
