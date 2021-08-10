using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] private GameObject TxNowLoading;

    [SerializeField] private string NextSceneName;


    public void Load()
    {
        TxNowLoading.SetActive(true);
        SceneManager.LoadScene(NextSceneName);
    }

    public void LoadMenu()
    {
        TxNowLoading.SetActive(true);
        SceneManager.sceneLoaded += GotoMenu;
        SceneManager.LoadScene(NextSceneName);

    }
    public void LoadNextSceneWin()
    {
        TxNowLoading.SetActive(true);
        SceneManager.sceneLoaded += Win;
        SceneManager.LoadScene(NextSceneName);
    }
    public void LoadNextSceneLose()
    {
        TxNowLoading.SetActive(true);
        SceneManager.sceneLoaded += Lose;
        SceneManager.LoadScene(NextSceneName);
    }

    public void GotoMenu(Scene next, LoadSceneMode mode)
    {
        var x = GameObject.FindWithTag("Init").GetComponent<MenuInit>();
        x.SetInitMenu("Main");
        SceneManager.sceneLoaded -= GotoMenu;
    }

    private void Win(Scene next, LoadSceneMode mode)
    {
        var x = GameObject.FindWithTag("Init").GetComponent<MenuInit>();
        x.SetInitMenu("Result");
        SceneManager.sceneLoaded -= Win;
    }

    private void Lose(Scene next, LoadSceneMode mode)
    {
        var x = GameObject.FindWithTag("Init").GetComponent<MenuInit>();
        x.SetInitMenu("Quest");
        SceneManager.sceneLoaded -= Lose;
    }


}
