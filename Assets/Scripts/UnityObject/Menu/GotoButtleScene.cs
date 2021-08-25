using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GotoButtleScene : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] ButtleSceneData buttleSceneData;
    [SerializeField] Text StageTitle;

    public void Load()
    {
        SceneManager.LoadScene("Wave");
        SceneManager.sceneLoaded += DataSet;
    }

    private void DataSet(Scene next, LoadSceneMode mode)
    {
        Debug.Log("DataSet run.");
        var dataSet = GameObject.FindWithTag("DataSet").GetComponent<Dataset>();
        dataSet.Initialize(playerData, buttleSceneData,StageTitle.text);
        SceneManager.sceneLoaded -= DataSet;
    }

}
