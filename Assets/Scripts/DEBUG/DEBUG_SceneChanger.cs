using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEBUG_SceneChanger : MonoBehaviour
{
    // 列挙型の定義
    public enum SCENE
    {
        Title,
        Menu,
        Buttle
    }

    public SCENE GoToScene;

    public void SceneChange()
    {
        /*
            使い方
            1. 遷移を起こすオブジェクトにアタッチします
            2. インスペクターからGoToSceneを遷移したいシーン先を指定してください。
            3. 遷移を起こすイベントでSceneChangeを指定してください。
        */
        SceneManager.LoadScene(GoToScene.ToString());
    }
}
