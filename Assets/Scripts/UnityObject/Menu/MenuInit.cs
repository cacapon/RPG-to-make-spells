using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuInit : MonoBehaviour
{
    [SerializeField] List<GameObject> InitMenu;
    [SerializeField] List<GameObject> Menus;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach(GameObject HideObj in Menus)
        {
            HideObj.SetActive(false);
        }
    }

    public void SetInitMenu(string initMenu)
    {
        switch(initMenu)
        {
            case "Main":
                InitMenu[0].SetActive(true);
                break;
            case "Quest":
                InitMenu[1].SetActive(true);
                break;
            case "Result":
                InitMenu[2].SetActive(true);
                break;
            default:
                throw new ArgumentException($"不正なメニュー名:{initMenu}");
        }
    }
}
