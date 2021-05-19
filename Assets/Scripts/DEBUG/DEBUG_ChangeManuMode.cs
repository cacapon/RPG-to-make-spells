using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEBUG_ChangeManuMode : MonoBehaviour
{

    [SerializeField]
    private GameObject Mode;

    private GameObject[] ModeName;

    private void GetAllChildObject()
    {
        ModeName = new GameObject[Mode.transform.childCount];

        for (int i = 0; i < Mode.transform.childCount; i++)
        {
            ModeName[i] = Mode.transform.GetChild(i).gameObject;
        }
    }

    private void Start()
    {
        GetAllChildObject();
        ChildInvisible();
        //Mainだけ表示
        ModeName[0].SetActive(true);
    }

    private void ChildInvisible()
    {
        //子オブジェクトを全て非表示にする。
        for (int i = 0; i < Mode.transform.childCount; i++)
        {
            ModeName[i].SetActive(false);
        }
    }

    public void ChangeMainMode()
    {
        ChildInvisible();
        ModeName[0].SetActive(true);
    }

    public void ChangeBookMode()
    {
        ChildInvisible();
        ModeName[1].SetActive(true);
    }

    public void ChangeQuestMode()
    {
        ChildInvisible();
        ModeName[2].SetActive(true);
    }
    public void ChangeConfigMode()
    {
        ChildInvisible();
        ModeName[3].SetActive(true);
    }

}
