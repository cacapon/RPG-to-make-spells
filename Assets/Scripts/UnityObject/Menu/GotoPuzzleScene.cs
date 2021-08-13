using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoPuzzleScene : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

    public void Load()
    {
        SceneManager.LoadScene("BookEdit");
        SceneManager.sceneLoaded += DataSet;
    }

    private void DataSet(Scene next, LoadSceneMode mode)
    {
        var dataSet = GameObject.FindWithTag("DataSet").GetComponent<BookEditDataSet>();
        dataSet.Initialize(playerData);
        SceneManager.sceneLoaded -= DataSet;
    }

}
