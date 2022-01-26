using System;
using System.Collections.Generic;
using UnityEngine;


public class TestBookEditStageManager : MonoBehaviour
{
    public TestBookEditSceneData data;

    public void HoldUpParts(Vector2Int targetPos)
    {
        //stageからHoldへ持ち上げます。
        if (data.HoldParts.IsActive) { return; } //持ち上げ中なら実行しない
        if (data.GroundStage.GetTile(targetPos).MyTile == eTile.None) { return; } //対象がNoneのエリアでも実行しない

        //targetに含まれるユニークIDから持ち上げるパーツの位置を特定する

        Guid targetID = data.GroundStage.GetTile(targetPos).MyUniqueID;
        Vector2Int[] holdUpPos = GetHoldUpShape(targetPos);
        eTile tile = data.GroundStage.GetTile(targetPos).MyTile;

        //Stageから転記していく
        data.HoldStage.Reset();

        foreach (Vector2Int cell in holdUpPos)
        {
            data.HoldStage.TileUpdate(cell, tile, targetID);
            data.GroundStage.TileUpdate(cell, eTile.None, Guid.Empty);
        }

        data.HoldParts.SetParts(holdUpPos, tile, targetID);

    }

    private Vector2Int[] GetHoldUpShape(Vector2Int targetpos)
    {
        Guid targetID = data.GroundStage.GetTile(targetpos).MyUniqueID;
        List<Vector2Int> tmpShapeList = new List<Vector2Int>() { targetpos };
        List<Vector2Int> next = new List<Vector2Int>() { targetpos };
        List<Vector2Int> clear = new List<Vector2Int>() { targetpos };

        Vector2Int searchPos;
        while (next.Count != 0)
        {
            searchPos = next[0];
            next.RemoveAt(0);

            //条件1 探索済みでない事
            //条件2 上下左右にtargetIDが含まれること
            AddShapeList(targetID, tmpShapeList, next, clear, searchPos + Vector2Int.up);
            AddShapeList(targetID, tmpShapeList, next, clear, searchPos + Vector2Int.down);
            AddShapeList(targetID, tmpShapeList, next, clear, searchPos + Vector2Int.left);
            AddShapeList(targetID, tmpShapeList, next, clear, searchPos + Vector2Int.right);
        }

        return tmpShapeList.ToArray();
    }

    private void AddShapeList(Guid targetID, List<Vector2Int> tmpShapeList, List<Vector2Int> next, List<Vector2Int> clear, Vector2Int check)
    {
        // この関数は以下の機能を有します
        //  1 対象の座標を探索済みにします
        //  2 対象の座標のIDがターゲットのIDと同じ場合shapelistと次回の探索候補に追加します

        if (IsOverRange(check)) { return; }   // 範囲外の場合は実行しない


        if (!clear.Contains(check))
        {
            clear.Add(check);
            if (data.GroundStage.GetTile(check).MyUniqueID == targetID)
            {
                next.Add(check);
                tmpShapeList.Add(check);
            }
        }
    }

    private bool IsOverRange(Vector2Int pos)
    {
        //対象の座標が範囲外かチェックします
        Vector2Int stageRangeMin = Vector2Int.zero;
        Vector2Int stageRangeMax = new Vector2Int(data.StageSize, data.StageSize) - Vector2Int.one;

        if (pos.x < stageRangeMin.x || pos.y < stageRangeMin.y ||
            pos.x > stageRangeMax.x || pos.y > stageRangeMax.y)
        {
            return true;
        }

        return false;
    }

    public void PutHoldParts()
    {
        //Holdしているパーツを置きます。
        if (!data.HoldParts.IsActive) { return; }    // Holdしてなかったら実行しない
        if (IsOverlapping()) { return; }        // 重複している個所があったら実行しない


        //Holdから転記していく
        for (int height = 0; height < data.StageSize; height++)
        {
            for (int width = 0; width < data.StageSize; width++)
            {
                Vector2Int pos = new Vector2Int(width, height);
                if (data.HoldStage.GetTile(pos).MyTile != eTile.None)
                {
                    data.GroundStage.TileUpdate(pos, data.HoldStage.GetTile(pos));
                }
            }
        }

        data.HoldParts.ReSetParts(); //置いたのでホールドパーツをリセットする
        //TODO: インベントリのボタンを再有効化する必要がある→どこでやる？
    }

    private bool IsOverlapping()
    {
        // 重なってたらtrue,それ以外はFalseを返します。
        for (int height = 0; height < data.StageSize; height++)
        {
            for (int width = 0; width < data.StageSize; width++)
            {
                // Hold
                Vector2Int pos = new Vector2Int(width, height);
                if (data.HoldStage.GetTile(pos).MyTile != eTile.None &&
                    data.GroundStage.GetTile(pos).MyTile != eTile.None)
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

        if (!data.HoldParts.IsActive) { return; }    // HoldPartsに何も設置されてなかったら何もしない


        // shapeをポインターに合わせてあるステージを中心に合わせて置いていきます
        Vector2Int[] shapePos = FixBorderPos(SetSenter(data.HoldParts.MyShape, centerPos));

        data.HoldStage.Reset();

        foreach (Vector2Int cell in shapePos)
        {
            //セルに情報を置いていきます。
            data.HoldStage.TileUpdate(cell,data.HoldParts.MyTile, data.HoldParts.MyUniqueID);
        }

    }

    private Vector2Int[] FixBorderPos(Vector2Int[] shapePos)
    {
        //範囲外対策 境界の外にあるデータを境界内まで戻す。

        Vector2Int minPos = GetMinVec(shapePos);
        Vector2Int maxPos = GetMaxShape(shapePos) - Vector2Int.one; // shapeを(-1,-1)した値がmaxPosになるため

        //ずらす移動量を決める
        Vector2Int fixPos = GetFixPos(minPos, maxPos);

        for (int i = 0; i < shapePos.Length; i++)
        {
            Debug.Log($"{i + 1};{shapePos[i]}");
            shapePos[i] += fixPos;
        }

        return shapePos;
    }

    private Vector2Int GetFixPos(Vector2Int shapeMinPos, Vector2Int shapeMaxPos)
    {
        //(0,0)~(stage.x-1,stage.y-1)の範囲に収まるよう移動量を求めます
        Vector2Int fixPos = Vector2Int.zero;
        Vector2Int stageRangeMin = Vector2Int.zero;
        Vector2Int stageRangeMax = new Vector2Int(data.StageSize, data.StageSize) - Vector2Int.one;

        //minPos < 0
        //maxPos >= data.StageSize

        if (shapeMinPos.x < stageRangeMin.x) { fixPos.x = stageRangeMin.x - shapeMinPos.x; }
        if (shapeMinPos.y < stageRangeMin.y) { fixPos.y = stageRangeMin.y - shapeMinPos.y; }
        if (shapeMaxPos.x >= stageRangeMax.x) { fixPos.x = stageRangeMax.x - shapeMaxPos.x; }
        if (shapeMaxPos.y >= stageRangeMax.y) { fixPos.y = stageRangeMax.y - shapeMaxPos.y; }

        return fixPos;
    }

    public void SetHoldStage(Vector2Int[] shape, eTile tile)
    {
        // shapeを中心(3,3)に合わせて置いていきます
        data.HoldParts.SetParts(shape, tile, Guid.NewGuid());

        Vector2Int[] centerShape = SetSenter(shape, new Vector2Int(3, 3));

        data.HoldStage.Reset();

        foreach (Vector2Int cell in centerShape)
        {
            Debug.Log(cell);
            //セルに情報を置いていきます。
            data.HoldStage.TileUpdate(cell,tile, data.HoldParts.MyUniqueID);
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
}