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
}
