using System;
using UnityEngine;


public class TestBookEditStageManager : MonoBehaviour
{
    // TODO: 配置ステージを準備     7x7
    // TODO: 持ち上げステージを準備 7x7

    private int STAGE_SIZE = 7;
    private eTile[,] Stage;
    private eTile[,] HoldStage;

    private Parts HoldParts;

    public int GetStageSize { get => STAGE_SIZE; }

    private void Awake()
    {
        Stage = InitStage();
        HoldStage = InitStage();
        HoldParts = new Parts();
    }

    private eTile[,] InitStage()
    {
        eTile[,] tmpStage = new eTile[STAGE_SIZE, STAGE_SIZE];

        for (int height = 0; height < STAGE_SIZE; height++)
        {
            for (int width = 0; width < STAGE_SIZE; width++)
            {
                tmpStage[height, width] = eTile.None;
            }
        }
        return tmpStage;
    }

    public eTile[,] GetStage(bool isHold)
    {
        if (isHold) { return HoldStage; }
        else { return Stage; }
    }

    public void MoveHoldStage(Vector2Int centerPos)
    {
        // TODO:範囲外の場合動かないために操作性が悪い。範囲外の場合でも動かせる方向には動かせるようにしたい。
        // HoldPartsに何も設置されてなかったら何もしない
        if(!HoldParts.IsActive){ return; }
        // shapeを中心に合わせて置いていきます
        Vector2Int[] shapePos = SetSenter(HoldParts.MyShape, centerPos);

        //範囲外対策 7x7なら(0,0)~(6,6)までに制限
        Vector2Int minPos = GetMinVec(shapePos);
        Vector2Int maxPos = GetMaxVec(shapePos);

        Debug.Log($"min:{minPos} max:{maxPos}");

        if (minPos.x <= 0 || minPos.y <= 0 ||
            maxPos.x > STAGE_SIZE || maxPos.y > STAGE_SIZE)
        {
            //範囲外なので何もしない
            return;
        }

        HoldStage = InitStage();

        foreach (Vector2Int cell in shapePos)
        {
            //セルに情報を置いていきます。
            HoldStage[cell.y, cell.x] = HoldParts.MyTile;
        }

    }

    public void SetHoldStage(Vector2Int[] shape, eTile tile)
    {
        // shapeを中心(3,3)に合わせて置いていきます
        HoldParts.SetParts(shape, tile);

        Vector2Int[] centerShape = SetSenter(shape, new Vector2Int(3, 3));

        HoldStage = InitStage();

        foreach (Vector2Int cell in centerShape)
        {
            Debug.Log(cell);
            //セルに情報を置いていきます。
            HoldStage[cell.y, cell.x] = tile;
        }
    }

    private Vector2Int[] SetSenter(Vector2Int[] shape, Vector2Int centerPos)
    {
        // Shapeの最大値が次の場合、中心位置はcenterPosに対してxとyそれぞれの基準点を追加したものになります
        // x,yの基準点は以下の通り
        //      1 -> 0
        //      2 -> 0
        //      3 -> -1
        //      4 -> -1

        Vector2Int maxVec = GetMaxVec(shape);
        Vector2Int basicPos = new Vector2Int(GetBasicPos(maxVec.x), GetBasicPos(maxVec.y));
        Vector2Int[] centerShape = new Vector2Int[shape.Length];
        Array.Copy(shape, centerShape, shape.Length);

        for (int i = 0; i < centerShape.Length; i++)
        {
            centerShape[i].x += centerPos.x + basicPos.x;
            centerShape[i].y += centerPos.y + basicPos.y;
        }

        return centerShape;

    }

    private static int GetBasicPos(int max)
    {
        switch (max)
        {
            case 1:
            case 2:
                return 0;
            case 3:
            case 4:
                return -1;
            default:
                throw new ArgumentException($"maxの値は1~4を想定しています maxVec:{max}");
        }
    }

    private static Vector2Int GetMaxVec(Vector2Int[] shape)
    {
        Vector2Int maxVec = Vector2Int.zero;
        foreach (Vector2Int cellPos in shape)
        {
            maxVec = Vector2Int.Max(maxVec, cellPos);
        }

        maxVec += Vector2Int.one;

        return maxVec;
    }

    private static Vector2Int GetMinVec(Vector2Int[] shape)
    {
        Vector2Int minVec = new Vector2Int(255, 255); //255は暫定値
        foreach (Vector2Int cellPos in shape)
        {
            minVec = Vector2Int.Min(minVec, cellPos);
        }

        minVec += Vector2Int.one;

        return minVec;
    }



    private void TestShow(eTile[,] stage)
    {
        for (int height = 0; height < STAGE_SIZE; height++)
        {
            string show_str = "";
            for (int width = 0; width < STAGE_SIZE; width++)
            {
                show_str += stage[height, width].ToString();
            }
            Debug.Log(show_str);
        }
        Debug.Log("");
    }
}

class Parts
{
    bool isActive = false;
    Vector2Int[] myShape;
    eTile myTile;

    public void SetParts(Vector2Int[] shape, eTile tile)
    {
        isActive = true;
        myShape = shape;
        myTile = tile;
    }

    public Vector2Int[] MyShape { get => myShape; }
    public eTile MyTile { get => myTile; }
    public bool IsActive { get => isActive;}
}