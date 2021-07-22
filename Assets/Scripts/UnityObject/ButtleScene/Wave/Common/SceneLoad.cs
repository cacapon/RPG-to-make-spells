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

    private void Win(Scene next, LoadSceneMode mode)
    {
        //勝った場合
        var Count = GameObject.FindWithTag("GameController").GetComponent<AdventureScript>();
        Count.Count = 42;
        SceneManager.sceneLoaded -= Win;
    }

    private void Lose(Scene next, LoadSceneMode mode)
    {
        //勝った場合
        var Count = GameObject.FindWithTag("GameController").GetComponent<AdventureScript>();
        Count.Count = 43;
        SceneManager.sceneLoaded -= Lose;
    }


}
