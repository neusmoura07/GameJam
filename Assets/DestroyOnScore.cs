using UnityEngine;

public class DestroyOnScore : MonoBehaviour
{
    // Score necessário para destruir o objeto
    public int targetScore = 3;

    private coleta coletaScript; // Referência ao script de coleta

    void Start()
    {
        // Busca o script de coleta no mesmo objeto
        coletaScript = GetComponent<coleta>();
    }

    void Update()
    {
        // Verifica se a referência ao script coleta existe e o score atingiu o valor necessário
        if (coletaScript != null && coletaScript.score >= targetScore)
        {
            Destroy(gameObject); // Destroi o objeto
        }
    }
}