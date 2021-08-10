using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeManuMode : MonoBehaviour
{

    [SerializeField] private List<GameObject> MenuMode;
    private void Invisible()
    {
        //全て非表示にする。
        foreach(GameObject obj in MenuMode)
        {
            obj.SetActive(false);
        }
    }

    #region Change Mode
    public void Change(GameObject obj)
    {
        if(!MenuMode.Contains(obj))
        {
            throw new ArgumentException("メニューでは無いオブジェクトが選択されました");
        }
        Invisible();
        obj.SetActive(true);
    }
    #endregion

}
