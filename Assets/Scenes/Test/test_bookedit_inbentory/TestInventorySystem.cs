using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestInventorySystem : MonoBehaviour, IDropHandler
{
    [SerializeField] private TextAsset Jsonfile;
    [SerializeField] private Text TxtDirPath; // パス表示部分
    [SerializeField] private GameObject InventoryItemsObj;
    [SerializeField] private GameObject DandDObj;
    [SerializeField] private TestBookEditStageManager bookEditStgMng;

    private List<GameObject> InventoryItemObjList;
    private Dictionary<eIcon, Sprite> spriteDict;
    private List<InventoryItem> InventoryItems;
    private List<string> path = new List<string>() { "~" }; //最長半角16文字まで
    private Sprite[] sprites;

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
        sprites = Resources.LoadAll<Sprite>("TestData/test_bookedit_inbentory/miniparts");
        spriteDict = new Dictionary<eIcon, Sprite>();
        eIcon i = 0;
        foreach (Sprite sprite in sprites)
        {
            spriteDict.Add(i, sprite);
            i++;
        }
    }

    private void GetInventoryObj()
    {

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
        }
    }

    private void InitJsonData()
    {
        string json = Jsonfile.text;
        var inventoryItemsJson = JsonHelper.FromJson<InventoryItemJson>(json);
        InventoryItems = new List<InventoryItem>();

        foreach (var itemjson in inventoryItemsJson)
        {
            InventoryItem item = new InventoryItem(itemjson);
            InventoryItems.Add(item);
        }
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

    private InventoryItem GetInventoryItem(string name)
    {
        //nameからJsonを問い合わせて一致するInventyを返す
        IEnumerable<InventoryItem> DirWhere = InventoryItems.Where(o => o.name == name);

        foreach (InventoryItem item in DirWhere)
        {
            // HACK:先頭だけ取り出したいけどIEnumerableの先頭の取り出し方が分からないので、Foreachで取り出しています。
            return item;
        }

        // 上手くヒットしない場合はエラーにする
        throw new ArgumentException();
    }

    private bool IsDirectory(string name)
    {
        //nameからJsonを問い合わせてtypeがディレクトリならTrueを返す
        IEnumerable<InventoryItem> DirWhere = InventoryItems.Where(o => o.name == name);

        foreach (InventoryItem item in DirWhere)
        {
            // HACK:先頭だけ取り出したいけどIEnumerableの先頭の取り出し方が分からないので、Foreachで取り出しています。
            return item.icon == eIcon.directory;
        }

        // 上手くヒットしない場合はエラーにする
        throw new ArgumentException();
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
        InventoryItem item = GetInventoryItem(partsName);
        if (!item.useItem) { return; }

        //ステージにパーツを置く
        item.useItem = false;
        bookEditStgMng.SetHoldStage(item.shape, item.tile);

        ListSegments();
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
            InventoryItemObjList[i].SetActive(true);
            InventoryItemObjList[i].name = item.name;

            InventoryItemObjList[i].GetComponent<Button>().interactable = item.useItem;             //インベントリアイテム有効化設定
            InventoryItemObjList[i].transform.GetChild(0).GetComponent<Text>().text = item.name;
            InventoryItemObjList[i].GetComponent<Image>().sprite = spriteDict[item.icon];

            Debug.Log($"{item.path} {item.name} {item.icon} {item.tile} {item.shape} ");
            i++;
        }
    }

    private void ChangeDirectory(string next_path)
    {
        // パスを変更する
        // TODO ホームに行く場合
        if (next_path == "..")
        {
            // 一つ上の階層に行く場合
            if (path.Count >= 2)
            {
                path.RemoveAt(path.Count - 1);
            }
        }
        else if (next_path == "//")
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

    public void OnDrop(PointerEventData eventData)
    {
        // TODO: imageを消したため、現在機能してません。
        // Debug.Log("Ondrop");
        // if (eventData.pointerDrag != null)
        // {
        //     //戻されたパーツのアイコンを再度押せるようにしてから動かせるイメージを削除
        //     eventData.pointerDrag.GetComponent<TestMoveInventoryImage>().MyItem.useItem = true;
        //     Destroy(eventData.pointerDrag);
        // }
    }
}


[Serializable]
public class InventoryItemJson
{
    public string path;
    public string name;
    public string icon;
    public string tile;
    public string shape;

}

public class InventoryItem
{
    public bool useItem = true;
    public string path;
    public string name;
    public eIcon icon;
    public eTile tile;
    public Vector2Int[] shape;

    public InventoryItem(InventoryItemJson jsondata)
    {
        this.path = jsondata.path;
        this.name = jsondata.name;
        this.icon = (eIcon)Enum.Parse(typeof(eIcon), jsondata.icon);
        this.tile = (eTile)Enum.Parse(typeof(eTile), jsondata.tile);
        this.shape = ConvertShape((eShape)Enum.Parse(typeof(eShape), jsondata.shape));
    }

    private Vector2Int[] ConvertShape(eShape shapeid)
    {
        switch (shapeid)
        {
            case eShape.None:
                return new Vector2Int[] {}; //directoryなど形が必要がないものはNoneとして扱う
            case eShape.DOT1:
                return new Vector2Int[] { new Vector2Int(0, 0) };
            case eShape.I2:
                return new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1) };
            case eShape.I3:
                return new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2) };
            case eShape.L3:
                return new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1) };
            case eShape.O4:
                return new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(1, 1) };
            case eShape.T4:
                return new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(0, 2) };
            case eShape.I4:
                return new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(0, 3) };
            case eShape.L4:
                return new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2), new Vector2Int(1, 2) };
            case eShape.J4:
                return new Vector2Int[] { new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, 2), new Vector2Int(1, 2) };
            case eShape.Z4:
                return new Vector2Int[] { new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 0) };
            case eShape.S4:
                return new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 1) };
            default:
                throw new ArgumentException($"想定外の値:{shapeid}");
        }
    }
}

[Serializable]
public enum eIcon
{
    directory,
    DOT1_B,
    I2_B,
    I3_B,
    L3_B,
    O4_B,
    T4_B,
    I4_B,
    L4_B,
    J4_B,
    Z4_B,
    S4_B,
    DOT1_B_PLUS,
    I2_B_PLUS,
    I3_B_PLUS,
    L3_B_PLUS,
    O4_B_PLUS,
    T4_B_PLUS,
    I4_B_PLUS,
    L4_B_PLUS,
    J4_B_PLUS,
    Z4_B_PLUS,
    S4_B_PLUS,
    DOT1_R,
    I2_R,
    I3_R,
    L3_R,
    O4_R,
    T4_R,
    I4_R,
    L4_R,
    J4_R,
    Z4_R,
    S4_R,
    DOT1_R_PLUS,
    I2_R_PLUS,
    I3_R_PLUS,
    L3_R_PLUS,
    O4_R_PLUS,
    T4_R_PLUS,
    I4_R_PLUS,
    L4_R_PLUS,
    J4_R_PLUS,
    Z4_R_PLUS,
    S4_R_PLUS,
    DOT1_Y,
    I2_Y,
    I3_Y,
    L3_Y,
    O4_Y,
    T4_Y,
    I4_Y,
    L4_Y,
    J4_Y,
    Z4_Y,
    S4_Y,
    DOT1_Y_PLUS,
    I2_Y_PLUS,
    I3_Y_PLUS,
    L3_Y_PLUS,
    O4_Y_PLUS,
    T4_Y_PLUS,
    I4_Y_PLUS,
    L4_Y_PLUS,
    J4_Y_PLUS,
    Z4_Y_PLUS,
    S4_Y_PLUS,
}

[Serializable]
public enum eTile
{
    None,
    BLUE,
    BLUE_PLUS,
    RED,
    RED_PLUS,
    YELLOW,
    YELLOW_PLUS,
}

[Serializable]
public enum eShape
{
    None,
    DOT1,
    I2,
    I3,
    L3,
    O4,
    T4,
    I4,
    L4,
    J4,
    Z4,
    S4
}