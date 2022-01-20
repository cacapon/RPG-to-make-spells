using System;
using UnityEngine;


public class TestBookEditStageManager : MonoBehaviour
{
    // TODO: 配置ステージを準備     7x7
    // TODO: 持ち上げステージを準備 7x7

    private int STAGE_SIZE = 7;
    private eTile[,] Stage;
    private eTile[,] HoldStage;

    private void Awake()
    {
        Stage = InitStage();
        HoldStage = InitStage();
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

    public void SetHoldStage(Vector2Int[] shape, eTile tile)
    {
        // shapeを中心(3,3)に合わせて置いていきます
        Vector2Int[] centerShape = SetSenter(shape);

        HoldStage = InitStage();

        foreach (Vector2Int cell in centerShape)
        {
            Debug.Log(cell);
            //セルに情報を置いていきます。
            HoldStage[cell.y, cell.x] = tile;
        }
    }

    private Vector2Int[] SetSenter(Vector2Int[] shape)
    {
        // Shapeの最大値が次の場合、xとyそれぞれの基準点を次の通りにします
        //      1 -> 3
        //      2 -> 3
        //      3 -> 2
        //      4 -> 2
        Vector2Int maxVec = GetMaxVec(shape);
        Vector2Int centerPos = new Vector2Int(GetCenterPos(maxVec.x), GetCenterPos(maxVec.y));
        Vector2Int[] centerShape = new Vector2Int[shape.Length];
        Array.Copy(shape, centerShape, shape.Length);

        for (int i = 0; i < centerShape.Length; i++)
        {
            centerShape[i].x += centerPos.x;
            centerShape[i].y += centerPos.y;
        }

        return centerShape;

    }

    private static int GetCenterPos(int max)
    {
        switch (max)
        {
            case 1:
            case 2:
                return 3;
            case 3:
            case 4:
                return 2;
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
