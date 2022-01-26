using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestInventorySystem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TestBookEditSceneData data;
    [SerializeField] private Text TxtDirPath; // パス表示部分
    [SerializeField] private GameObject InventoryItemsObj;
    [SerializeField] private TestBookEditStageManager bookEditStgMng;

    private List<GameObject> InventoryItemObjList;

    void Start()
    {
        InitInventoryObj();
        PrintWorkingDirectory();
        ListSegments();
    }

    private void InitInventoryObj()
    {

        // 子オブジェクトを返却する配列作成
        InventoryItemObjList = new List<GameObject>();

        // 子オブジェクトを順番に配列に格納
        foreach (Transform child in InventoryItemsObj.transform)
        {
            InventoryItemObjList.Add(child.gameObject);
        }

        foreach (var item in InventoryItemObjList)
        {
            Text itemnameobj = item.transform.GetChild(0).GetComponent<Text>();
            Image itemImage = item.GetComponent<Image>();
        }
    }

    public void TapItem(Button sender)
    {
        if (data.InventoryItems.GetInventoryItem(sender.name).icon == eIcon.directory)
        {
            TapDirectory(sender.name);
        }
        else
        {
            TapParts(sender.name);
        }
    }

    public void TapHomeButton()
    {
        ChangeDirectory("//");
    }

    public void TapUpButton()
    {
        ChangeDirectory("..");
    }

    private void TapDirectory(string nextpath)
    {
        ChangeDirectory(nextpath);
    }

    private void TapParts(string partsName)
    {
        InventoryItem item = data.InventoryItems.GetInventoryItem(partsName);
        if (!item.useItem) { return; }
        if (data.HoldParts.IsActive) { return; }

        //ステージにパーツを置く
        item.useItem = false;
        Guid id = Guid.NewGuid();
        data.UsePartsDict.Register(id, item.name);
        bookEditStgMng.SetHoldStage(item.shape, item.tile, id);

        ListSegments();
    }


    private void PrintWorkingDirectory()
    {
        // 現在のpathを表示する
        TxtDirPath.text = data.Path.path;
    }

    private void ListSegments()
    {
        // 現在パスに含まれるデータの一覧をインベントリに表示する

        // 一度全て非表示にする
        foreach (var item in InventoryItemObjList)
        {
            item.SetActive(false);
        }

        // オブジェクトを割り当てる
        int i = 0;
        foreach (var item in data.InventoryItems.ListSegments(data.Path.path))
        {
            InventoryItemObjList[i].SetActive(true);
            InventoryItemObjList[i].name = item.name;

            InventoryItemObjList[i].GetComponent<Button>().interactable = item.useItem;             //インベントリアイテム有効化設定
            InventoryItemObjList[i].transform.GetChild(0).GetComponent<Text>().text = item.name;
            InventoryItemObjList[i].GetComponent<Image>().sprite = data.SpriteDict[item.icon];

            i++;
        }
    }

    private void ChangeDirectory(string next_path)
    {
        // パスを変更する
        data.Path.Add(next_path);
        PrintWorkingDirectory();
        ListSegments();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!data.HoldParts.IsActive) { return; } //もってない場合は実行しない

        string partsName = data.UsePartsDict.GetName(data.HoldParts.MyUniqueID);
        InventoryItem item = data.InventoryItems.GetInventoryItem(partsName);

        item.useItem = true;
        data.HoldParts.ReSetParts();
        data.HoldStage.Reset();
        data.UsePartsDict.UnRegister(partsName);
        ListSegments();
    }
}
