using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomedoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOptions;
    public void Jogar()
    {
        SceneManager.LoadScene(nomedoLevelDeJogo);
    }

    public void AbrirOptions()
    {
        painelMenuInicial.SetActive(false);
        painelOptions.SetActive(true);
    }

    public void FecharOptions()
    {
        painelMenuInicial.SetActive(true);
        painelOptions.SetActive(false);
    }

    public void SairJogo()
    {
        
        Application.Quit();
        Debug.Log("Sair do Jogo");
    }

    public void VoltarMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
        Time.timeScale = 1f; // Certifique-se de que o tempo está rodando normalmente

        // Reset de estados manuais (se necessário)
        if (player.Instance != null)
        {
            Destroy(player.Instance.gameObject); // Garantir que o jogador será recriado
        }
    }
}
