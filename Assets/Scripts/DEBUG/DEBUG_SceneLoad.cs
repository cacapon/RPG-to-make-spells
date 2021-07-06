using UnityEngine;
using UnityEngine.SceneManagement;

public class DEBUG_SceneLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject TxNowLoading;
    public void LoadButtleScene()
    {
        TxNowLoading.SetActive(true);
        SceneManager.LoadScene("test_Wave");
    }

    public void LoadTitle()
    {
        TxNowLoading.SetActive(true);
        SceneManager.LoadScene("test_WaveGameStart");
    }

}
