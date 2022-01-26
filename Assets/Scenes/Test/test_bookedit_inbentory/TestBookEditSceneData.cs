using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestBookEditSceneData : MonoBehaviour
{
    //BookEditScene内のデータを管理します
    #region Public Data
    public Dictionary<eIcon, Sprite> SpriteDict { get => spriteDict; }
    public InventoryItems InventoryItems { get => inventoryItems; }
    public Stage GroundStage { get => groundStage; }
    public Stage HoldStage { get => holdStage; }
    public int StageSize { get => stageSize; }
    public Parts HoldParts { get => holdParts; }

    public Path Path;

    #endregion

    #region inventory
    [SerializeField] private TextAsset jsonfile;
    private InventoryItems inventoryItems;
    private Dictionary<eIcon, Sprite> spriteDict;

    #endregion

    #region stage
    private Stage groundStage;
    private Stage holdStage;
    private int stageSize = 7;
    private Parts holdParts;

    #endregion

    private void Awake()
    {
        // inventory
        InitSpriteDict();
        InitJsonData();
        Path = new Path();

        // stage
        groundStage = new Stage(stageSize);
        holdStage = new Stage(stageSize);
        holdParts = new Parts();
    }

    private void InitSpriteDict()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("TestData/test_bookedit_inbentory/miniparts");
        spriteDict = new Dictionary<eIcon, Sprite>();
        eIcon i = 0;
        foreach (Sprite sprite in sprites)
        {
            spriteDict.Add(i, sprite);
            i++;
        }
    }

    private void InitJsonData()
    {
        string json = jsonfile.text;
        var inventoryItemsJson = JsonHelper.FromJson<InventoryItemJson>(json);
        var tmpitems = new List<InventoryItem>();

        foreach (var itemjson in inventoryItemsJson)
        {
            InventoryItem item = new InventoryItem(itemjson);
            tmpitems.Add(item);
        }
        inventoryItems = new InventoryItems(tmpitems);
    }
}

public class Stage
{
    private StageTile[,] map;

    private int stagesize;

    public Stage(int size)
    {
        stagesize = size;
        map = InitStage();
    }

    public StageTile GetTile(int height, int width)
    {
        return map[height, width];
    }

    public StageTile GetTile(Vector2Int pos)
    {
        return map[pos.y, pos.x];
    }

    public void TileUpdate(Vector2Int pos, StageTile stagetile)
    {
        map[pos.y, pos.x].TileUpdate(stagetile.MyTile, stagetile.MyUniqueID);
    }

    public void TileUpdate(Vector2Int pos, eTile nextTile, Guid nextID)
    {
        map[pos.y, pos.x].TileUpdate(nextTile, nextID);
    }
    public void Reset()
    {
        map = InitStage();
    }

    private StageTile[,] InitStage()
    {
        StageTile[,] tmpStage = new StageTile[stagesize, stagesize];

        for (int height = 0; height < stagesize; height++)
        {
            for (int width = 0; width < stagesize; width++)
            {
                tmpStage[height, width] = new StageTile();
            }
        }
        return tmpStage;
    }

    public class StageTile
    {
        eTile myTile;
        Guid myUniqueID;

        public StageTile()
        {
            myTile = eTile.None;
            myUniqueID = Guid.Empty;
        }

        public eTile MyTile { get => myTile; }
        public Guid MyUniqueID { get => myUniqueID; }

        public void TileUpdate(eTile nextTile, Guid nextID)
        {
            myTile = nextTile;
            myUniqueID = nextID;
        }
    }
}

public class Parts
{
    bool isActive = false;
    Vector2Int[] myShape;
    eTile myTile;
    Guid myUniqueID;

    public void SetParts(Vector2Int[] shape, eTile tile, Guid myID)
    {
        isActive = true;
        myShape = OrthopaedyShape(shape);
        myTile = tile;
        myUniqueID = myID;
    }
    public void ReSetParts()
    {
        isActive = false;
        myShape = null;
        myTile = eTile.None;
        myUniqueID = Guid.Empty;
    }

    private Vector2Int[] OrthopaedyShape(Vector2Int[] shape)
    {
        //座標を0基準に整形しなおします。
        Vector2Int minpos = new Vector2Int(255, 255);

        foreach (Vector2Int cell in shape)
        {
            minpos.x = Mathf.Min(minpos.x, cell.x);
            minpos.y = Mathf.Min(minpos.y, cell.y);
        }

        List<Vector2Int> tmpShape = new List<Vector2Int>();

        foreach (Vector2Int cell in shape)
        {
            tmpShape.Add(cell - minpos);
        }

        return tmpShape.ToArray();
    }

    public Vector2Int[] MyShape { get => myShape; }
    public eTile MyTile { get => myTile; }
    public bool IsActive { get => isActive; }
    public Guid MyUniqueID { get => myUniqueID; }
}



public class Path
{
    private List<string> pathlist = new List<string>() { "~" }; //最長半角16文字まで TODO:最長制限処理未実装

    public string path { get => string.Join("/", pathlist); }

    public void Add(string path)
    {
        switch (path)
        {
            case "//":
                GotoHome();
                break;
            case "..":
                GotoUpperDir();
                break;
            default:
                GotoDir(path);
                break;
        }
    }
    private void GotoHome()
    {
        pathlist.Clear();
        pathlist.Add("~");
    }

    private void GotoUpperDir()
    {
        if (pathlist.Count >= 2)
        {
            pathlist.RemoveAt(pathlist.Count - 1);
        }
    }
    private void GotoDir(string pathStr)
    {
        pathlist.Add(pathStr);
    }
}

public class InventoryItems
{
    private List<InventoryItem> items;

    public InventoryItems(List<InventoryItem> items)
    {
        this.items = items;
    }

    public IEnumerable<InventoryItem> ListSegments(string path)
    {
        //指定したpathに含まれるインベントリのアイテムを返します。
        return items.Where(o => o.path == string.Join("/", path));
    }

    public InventoryItem GetInventoryItem(string name)
    {
        //指定した名前のインベントリアイテムを返します。
        return items.Where(o => o.name == name).First(); // 要素数0の場合　エラー ArgumentNullException になります。
    }
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
                return new Vector2Int[] { }; //directoryなど形が必要がないものはNoneとして扱う
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
public class InventoryItemJson
{
    public string path;
    public string name;
    public string icon;
    public string tile;
    public string shape;

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