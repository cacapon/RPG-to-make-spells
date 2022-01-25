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

    public void PutHoldParts()
    {
        //Holdしているパーツを置きます。
        if (!HoldParts.IsActive) { return; }    // Holdしてなかったら実行しない
        if (IsOverlapping()) { return; }        // 重複している個所があったら実行しない


        //Holdから転記していく
        for (int height = 0; height < STAGE_SIZE; height++)
        {
            for (int width = 0; width < STAGE_SIZE; width++)
            {
                if(HoldStage[height,width] != eTile.None)
                {
                    Stage[height,width] = HoldStage[height,width];
                }
            }
        }

        HoldParts.ReSetParts(); //置いたのでホールドパーツをリセットする
        //TODO: インベントリのボタンを再有効化する必要がある→どこでやる？
    }

    private bool IsOverlapping()
    {
        // 重なってたらtrue,それ以外はFalseを返します。
        for (int height = 0; height < STAGE_SIZE; height++)
        {
            for (int width = 0; width < STAGE_SIZE; width++)
            {
                // Hold
                if (HoldStage[height, width] != eTile.None && Stage[height, width] != eTile.None)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void MoveHoldStage(Vector2Int centerPos)
    {
        // 触ったブロックを中心にして持ち上げているパーツを動かします。

        if (!HoldParts.IsActive) { return; }    // HoldPartsに何も設置されてなかったら何もしない


        // shapeをポインターに合わせてあるステージを中心に合わせて置いていきます
        Vector2Int[] shapePos = FixBorderPos(SetSenter(HoldParts.MyShape, centerPos));

        HoldStage = InitStage();

        foreach (Vector2Int cell in shapePos)
        {
            //セルに情報を置いていきます。
            HoldStage[cell.y, cell.x] = HoldParts.MyTile;
        }

    }

    private Vector2Int[] FixBorderPos(Vector2Int[] shapePos)
    {
        //範囲外対策 境界の外にあるデータを境界内まで戻す。

        Vector2Int minPos = GetMinVec(shapePos);
        Vector2Int maxPos = GetMaxShape(shapePos) - Vector2Int.one; // shapeを(-1,-1)した値がmaxPosになるため

        //ずらす移動量を決める
        Vector2Int fixPos = GetFixPos(minPos,maxPos);

        for (int i = 0; i < shapePos.Length; i++)
        {
            Debug.Log($"{i+1};{shapePos[i]}");
            shapePos[i] += fixPos;
        }

        return shapePos;
    }

    private Vector2Int GetFixPos(Vector2Int shapeMinPos, Vector2Int shapeMaxPos)
    {
        //(0,0)~(stage.x-1,stage.y-1)の範囲に収まるよう移動量を求めます
        Vector2Int fixPos = Vector2Int.zero;
        Vector2Int stageRangeMin = Vector2Int.zero;
        Vector2Int stageRangeMax = new Vector2Int(STAGE_SIZE,STAGE_SIZE) - Vector2Int.one;

        //minPos < 0
        //maxPos >= STAGE_SIZE

        if(shapeMinPos.x < stageRangeMin.x){ fixPos.x = stageRangeMin.x - shapeMinPos.x; }
        if(shapeMinPos.y < stageRangeMin.y){ fixPos.y = stageRangeMin.y - shapeMinPos.y; }
        if(shapeMaxPos.x >= stageRangeMax.x){ fixPos.x = stageRangeMax.x - shapeMaxPos.x; }
        if(shapeMaxPos.y >= stageRangeMax.y){ fixPos.y = stageRangeMax.y - shapeMaxPos.y; }

        return fixPos;
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
        //中心の座標に合わせたshapeの座標を返します。

        Vector2Int maxShape = GetMaxShape(shape);
        Vector2Int basicPos = new Vector2Int(GetBasicPos(maxShape.x), GetBasicPos(maxShape.y));
        Vector2Int[] centerShape = new Vector2Int[shape.Length];
        Array.Copy(shape, centerShape, shape.Length);

        for (int i = 0; i < centerShape.Length; i++)
        {
            centerShape[i].x += centerPos.x + basicPos.x;
            centerShape[i].y += centerPos.y + basicPos.y;
        }

        return centerShape;

    }

    private static int GetBasicPos(int maxShape)
    {
        // 形の大きさから基準点中心地点を求めます
        // 大きさに対する基準点は以下の通り
        // [大きさ] -> [基準点]
        // 1 -> 0
        // 2 -> 0
        // 3 -> -1
        // 4 -> -1

        switch (maxShape)
        {
            case 1:
            case 2:
                return 0;
            case 3:
            case 4:
                return -1;
            default:
                throw new ArgumentException($"maxの値は1~4を想定しています maxVec:{maxShape}");
        }
    }

    private static Vector2Int GetMaxShape(Vector2Int[] shape)
    {
        //与えられたShapeの大きさを取得します
        Vector2Int maxVec = Vector2Int.zero;
        foreach (Vector2Int cellPos in shape)
        {
            maxVec = Vector2Int.Max(maxVec, cellPos);
        }

        maxVec += Vector2Int.one; // 取得したのは0基準の座標なので＋１して大きさを取得できるようにしています

        return maxVec;
    }

private static Vector2Int GetMinVec(Vector2Int[] shape)
    {
        Vector2Int minVec = new Vector2Int(255, 255); //255は暫定値
        foreach (Vector2Int cellPos in shape)
        {
            minVec = Vector2Int.Min(minVec, cellPos);
        }

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
    public void ReSetParts()
    {
        isActive = false;
        myShape = null;
        myTile = eTile.None;
    }


    public Vector2Int[] MyShape { get => myShape; }
    public eTile MyTile { get => myTile; }
    public bool IsActive { get => isActive; }
}