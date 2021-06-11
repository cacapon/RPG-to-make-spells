using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book: MonoBehaviour
{
    //対象の本のページを切り替えるクラスになります。
    //子オブジェクトとして定義したページを切り替えます。

    [SerializeField]
    private GameObject TargetBook = default;

    private int NowPage = 0;


    private void Awake()
    {
        //インスペクターで指定した現在のページがページ全体を超えた値の場合
        if (NowPage >= TargetBook.transform.childCount - 1) 
        {
            Debug.LogError("ページ数の指定が子オブジェクトを超えています。 Now Page のインスペクター値を確認してください。");
            AppSystemManager.Quit();
        }


        //最初のページだけ表示します。
        for (int i = 0; i < TargetBook.transform.childCount; i++)
        {
            TargetBook.transform.GetChild(i).gameObject.SetActive(false);
        }

        TargetBook.transform.GetChild(NowPage).gameObject.SetActive(true);
    }

    public void UseMagic()
    {
        // TODO:後で実装する
        Debug.Log(TargetBook.transform.GetChild(NowPage).gameObject.name + "ページの魔法を使用します。");
    }

    public void Turn(bool isNext) 
    {
        int afterPage = 0;
        if (isNext)
        {
            if (NowPage == TargetBook.transform.childCount - 1) { return; }
            afterPage++;
        }
        else
        {
            if (NowPage == 0) { return; }
            afterPage--;
        }

        TargetBook.transform.GetChild(NowPage).gameObject.SetActive(false);
        NowPage += afterPage;
        TargetBook.transform.GetChild(NowPage).gameObject.SetActive(true);
    }
}
