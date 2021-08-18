using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class StageData : MonoBehaviour
{
    static private int stageSize = 7;
    public List<List<CellType>> Stage;

    private void Awake()
    {
        Stage = new List<List<CellType>>();

        for (int i = 0; i < stageSize; i++)
        {
            Stage.Add(new List<CellType>());
            for (int j = 0; j < stageSize; j++)
            {
                Stage[i].Add(CellType.None);
            }
        }
    }
    public string ShowStageData()
    {
        string stagestring = "";
        for (int i = 0; i < Stage.Count; i++)
        {
            for (int j = 0; j < Stage[i].Count; j++)
            {
                stagestring += (int)Stage[i][j];
            }
            stagestring += "\n";
        }
        return stagestring;
    }

    public void SetCell(List<List<CellType>> piecedata)
    {
        for (int v = 0; v < piecedata.Count; v++)
        {
            for (int h = 0; h < piecedata[v].Count; h++)
            {
                Stage[v][h] = piecedata[v][h];
            }
        }
    }

    public void Down()
    {
        //最下段に何かしらのピースがある場合は実行しない
        if (Stage[stageSize - 1].Any(value => value != CellType.None))
        {
            return;
        }

        //最下段を削除
        Stage.RemoveAt(stageSize - 1);

        //空の列を作成し、最上段に追加
        List<CellType> Row = new List<CellType>();
        for (int rowIndex = 0; rowIndex < stageSize; rowIndex++)
        {
            Row.Add(CellType.None);
        }
        Stage.Insert(0, Row);
    }

    public void Up()
    {
        //最上段に何かしらのピースがある場合は実行しない
        if (Stage[0].Any(value => value != CellType.None))
        {
            return;
        }

        //最上段を削除
        Stage.RemoveAt(0);

        //空の行を作成し、最下段に追加
        List<CellType> Row = new List<CellType>();
        for (int rowIndex = 0; rowIndex < stageSize; rowIndex++)
        {
            Row.Add(CellType.None);
        }
        Stage.Add(Row);
    }

    public void Right()
    {
        //右端列に何かピースがある場合は実行しない。
        for (int ColumnIndex = 0; ColumnIndex < stageSize; ColumnIndex++)
        {
            if (Stage[ColumnIndex][stageSize -1] != CellType.None)
            {
                return;
            }
        }

        //右端列を削除し、空の列を左列に追加
        for (int ColumnIndex = 0; ColumnIndex < stageSize; ColumnIndex++)
        {
            Stage[ColumnIndex].RemoveAt(stageSize - 1);
            Stage[ColumnIndex].Insert(0, CellType.None);
        }
    }

    public void Left()
    {
        //左端列に何かピースがある場合は実行しない。
        for (int ColumnIndex = 0; ColumnIndex < stageSize; ColumnIndex++)
        {
            if (Stage[ColumnIndex][0] != CellType.None)
            {
                return;
            }
        }

        //左端列を削除し、空の列を右列に追加
        for (int ColumnIndex = 0; ColumnIndex < stageSize; ColumnIndex++)
        {
            Stage[ColumnIndex].RemoveAt(0);
            Stage[ColumnIndex].Add(CellType.None);
        }
    }

}