using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEBUG_ChangeManuMode : MonoBehaviour
{

    [SerializeField]
    private GameObject Main;
    [SerializeField]
    private GameObject Book;
    [SerializeField]
    private GameObject Quest;
    [SerializeField]
    private GameObject Config;

    private void Start()
    {
        //最初にMainを表示する。
        ChangeMainMode();
    }

    private void Invisible()
    {
        //全て非表示にする。
        Main.SetActive(false);
        Book.SetActive(false);
        Quest.SetActive(false);
        Config.SetActive(false);
    }

    #region Change Mode
    //冗長だとは思うけど、後でいじった時に分かりにくくならないように、関数ごとに宣言してあります。
    public void ChangeMainMode()
    {
        Invisible();
        Main.SetActive(true);
    }

    public void ChangeBookMode()
    {
        Invisible();
        Book.SetActive(true);
    }

    public void ChangeQuestMode()
    {
        Invisible();
        Quest.SetActive(true);
    }
    public void ChangeConfigMode()
    {
        Invisible();
        Config.SetActive(true);
    }
    #endregion

}
