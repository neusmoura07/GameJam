using UnityEngine;

public class HideAfterSeconds : MonoBehaviour
{
    public float delay = 5f; // Tempo antes de esconder o painel

    void OnEnable()
    {
        // Começa a contagem para esconder o painel
        Invoke("HidePanel", delay);
    }

    void HidePanel()
    {
        gameObject.SetActive(false); // Desativa o painel
    }
}
