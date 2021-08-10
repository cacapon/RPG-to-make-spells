using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoPuzzleScene : MonoBehaviour
{
    //[SerializeField] PlayerData playerData;
    //[SerializeField] ButtleSceneData buttleSceneData;

    public void Load()
    {
        SceneManager.LoadScene("BookEdit");
        //SceneManager.sceneLoaded += DataSet;
    }

    /*
    private void DataSet(Scene next, LoadSceneMode mode)
    {
        Debug.Log("DataSet run.");
        var dataSet = GameObject.FindWithTag("DataSet").GetComponent<Dataset>();
        dataSet.Initialize(playerData, buttleSceneData);
        SceneManager.sceneLoaded -= DataSet;
    }
    */
}
