using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TestInventorySystem : MonoBehaviour
{
    [SerializeField] private TextAsset Jsonfile;
    [SerializeField] private Text TxtDirPath; // パス表示部分
    [SerializeField] private GameObject InventoryItemsObj;
    [SerializeField] private List<Sprite> sprites; //TODO:使いまわしに向かない　一枚の絵から自動で分割したい　eImageTypeとインスペクタの位置を合わせる事


    private List<GameObject> InventoryItemObjList;
    private Dictionary<eImageType,Sprite> spriteDict;
    private InventoryItem[] InventoryItems;
    private List<Text> names;

    private List<Image> images;
    private List<string> path = new List<string>() { "~" }; //最長半角16文字まで

    // Start is called before the first frame update
    void Start()
    {
        InitSpriteDict();
        InitJsonData();
        GetInventoryObj();
        PrintWorkingDirectory();
        ListSegments();
    }

    private void InitSpriteDict()
    {
        spriteDict = new Dictionary<eImageType, Sprite>();
        eImageType i = 0;
        foreach(Sprite sprite in sprites)
        {
            spriteDict.Add(i,sprite);
            i++;
        }
    }

    private void GetInventoryObj()
    {
        names = new List<Text>();
        images = new List<Image>();

        // 子オブジェクトを列挙する
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
            names.Add(itemnameobj);
            images.Add(itemImage);
        }
    }

    private void InitJsonData()
    {
        string json = Jsonfile.text;
        InventoryItems = JsonHelper.FromJson<InventoryItem>(json);
    }

    public void TapItem(Button sender)
    {

        if (IsDirectory(sender.name))
        {
            TapDirectory(sender.name);
        }
        else
        {
            TapParts(sender.name);
        }
    }

    private bool IsDirectory(string name)
    {
        //nameからJsonを問い合わせてtypeがディレクトリならTrueを返す
        IEnumerable<InventoryItem> DirWhere = InventoryItems.Where(o => o.name == name);

        foreach(InventoryItem item in DirWhere)
        {
            // HACK:先頭だけ取り出したいけどIEnumerableの先頭の取り出し方が分からないので、Foreachで取り出しています。
            return item.type == eObjectType.directory;
        }

        // 上手くヒットしない場合はエラーにする
        throw new ArgumentException();
    }


    public void TapHomeButton()
    {
        ChangeDirectory("//");
        ListSegments();
    }

    public void TapUpButton()
    {
        ChangeDirectory("..");
        ListSegments();
    }

    private void TapDirectory(string nextpath)
    {
        ChangeDirectory(nextpath);
        ListSegments();
    }

    private void TapParts(string partsName)
    {
        //ステージにパーツを置く
        //TODO
        Debug.Log(partsName);
    }


    private void PrintWorkingDirectory()
    {
        // 現在のpathを表示する
        TxtDirPath.text = string.Join("/", path);
    }

    private void ListSegments()
    {
        // 現在パスに含まれるデータの一覧をインベントリに表示する
        IEnumerable<InventoryItem> testWhere = InventoryItems.Where(o => o.path == string.Join("/", path));

        // 一度全て非表示にする
        foreach (var item in InventoryItemObjList)
        {
            item.SetActive(false);
        }

        // オブジェクトを割り当てる
        int i = 0;
        foreach (var item in testWhere)
        {
            //TODO:JSONとオブジェクトを紐づける
            InventoryItemObjList[i].SetActive(true);
            InventoryItemObjList[i].name = item.name;

            names[i].text = item.name;
            images[i].sprite = spriteDict[item.image];
            Debug.Log($"{item.path} {item.name} {item.type} {item.image}");
            i++;
        }
    }

    private Sprite SetSprite(eImageType type)
    {
        switch(type)
        {
            case eImageType.directory:
                return sprites[0];
        }
        return sprites[0];
    }

    private void ChangeDirectory(string next_path)
    {
        // パスを変更する
        // TODO ホームに行く場合
        if(next_path == "..")
        {
            // 一つ上の階層に行く場合
            if(path.Count >= 2)
            {
                path.RemoveAt(path.Count -1);
            }
        }
        else if(next_path == "//")
        {
            // ホームに行く場合
            path.Clear();
            path.Add("~");
        }
        else
        {
            // ディレクトリをタップした場合
            path.Add(next_path);
        }
        PrintWorkingDirectory();
        ListSegments();
    }
}

[Serializable]
public class InventoryItem
{
    public string path;
    public string name;
    public eObjectType type;
    public eImageType image;

}

public enum eObjectType
{
    parts = 0,
    directory = 1,
}

public enum eImageType
{
    directory,
    white,
    purple,
    red,
    blue,
    green,
    yellow,
    brown,
    white_plus,
    purple_plus,
    red_plus,
    blue_plus,
    green_plus,
    yellow_plus,
    brown_plus,
    directory_image,
}