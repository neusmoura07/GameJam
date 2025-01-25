using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
   public int Life = 7;
   public int TotalLife = 7;
   public Image[] Vidas;
   public player player;
    void Start()
    {
        totalLifeControl();
        LifeControl();
    }

    

    private void totalLifeControl()
    {
        for( int i = 0; i < Vidas.Length; i++)
        {
            if(i < TotalLife)
            {
                Vidas[i].enabled = true;
            }
            else
            {
                Vidas[i].enabled = false;
            }
        }
    }

    private void LifeControl()
    {
        for( int i = 0; i < Vidas.Length; i++)
        {
            if(i < Life)
            {
                Vidas[i].color = Color.white;
            }
            else
            {
                Vidas[i].color = Color.black;
            }
        }

        if (Life <= 0)
        {
            player.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Vida")
        {
            Destroy(collision.gameObject);
            AddVida();
        }
        
    }

    public void DropVidas()
    {
        if(Life > 0)
        {
            Life --;
        }
        LifeControl();
        
    }

    public void AddVida()
    {
        if(Life < TotalLife)
        {
            Life ++;
        }
        LifeControl(); 
    }

}
