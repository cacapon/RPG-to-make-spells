using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class StageData : MonoBehaviour
{
    static private int stageSize = 7;
    public List<List<(Guid,CellType)>> Stage;

    [SerializeField] bool isStage;

    private void Awake()
    {
        Init();
        if(isStage)
        {
            SetStartAndEndCell();
        }
    }

    public void Init()
    {
        Stage = new List<List<(Guid,CellType)>>();

        for (int i = 0; i < stageSize; i++)
        {
            Stage.Add(new List<(Guid,CellType)>());
            for (int j = 0; j < stageSize; j++)
            {
                Stage[i].Add((Guid.Empty,CellType.None));
            }
        }
    }

    public void SetStartAndEndCell()
    {
        Stage[0][0] = (Guid.NewGuid(),CellType.Start);
        Stage[stageSize-1][stageSize-1] = (Guid.NewGuid(),CellType.End);
    }
    public string ShowStageData()
    {
        string stagestring = "";
        for (int i = 0; i < Stage.Count; i++)
        {
            for (int j = 0; j < Stage[i].Count; j++)
            {
                stagestring += (int)Stage[i][j].Item2;
            }
            stagestring += "\n";
        }
        return stagestring;
    }

    public void SetCell(List<List<CellType>> piecedata)
    {
        Guid instantid = Guid.NewGuid();
        for (int v = 0; v < piecedata.Count; v++)
        {
            for (int h = 0; h < piecedata[v].Count; h++)
            {
                Stage[v][h] = (instantid,piecedata[v][h]);
            }
        }
    }
    public bool isEmpty()
    {
        for (int v = 0; v < Stage.Count; v++)
        {
            for (int h = 0; h < Stage[v].Count; h++)
            {
                if( Stage[v][h].Item2 != CellType.None){ return false; }
            }
        }
        return true;
    }

    public bool isEmpty(Vector2Int pos)
    {
        if( Stage[pos.y][pos.x].Item2 != CellType.None){ return false; }

        return true;
    }

    public bool isDeplicated(List<List<(Guid,CellType)>> anotherStage)
    {
        //対象のセルがどちらもNone以外のセルの場合Trueとする
        for (int v = 0; v < Stage.Count; v++)
        {
            for (int h = 0; h < Stage[v].Count; h++)
            {
                if( Stage[v][h].Item2         != CellType.None &&
                    anotherStage[v][h].Item2  != CellType.None)
                    {
                        Debug.Log("Deplicated");
                        return true;
                    }
            }
        }

        return false;
    }

    public void Put(List<List<(Guid,CellType)>> anotherStage)
    {
        for (int v = 0; v < Stage.Count; v++)
        {
            for (int h = 0; h < Stage[v].Count; h++)
            {
                if( anotherStage[v][h].Item2  != CellType.None)
                {
                    Stage[v][h] = anotherStage[v][h];
                }
            }
        }
    }

    public List<List<(Guid,CellType)>> Holdon(Vector2Int pos)
    {
        Guid targetGuid = Stage[pos.y][pos.x].Item1;
        List<List<(Guid,CellType)>> targetErea = new List<List<(Guid, CellType)>>();

        for (int v = 0; v < Stage.Count; v++)
        {
            targetErea.Add(new List<(Guid, CellType)>());
            for (int h = 0; h < Stage[v].Count; h++)
            {
                if( Stage[v][h].Item1  == targetGuid)
                {
                    targetErea[v].Add(Stage[v][h]);
                    Stage[v][h] = (Guid.Empty,CellType.None);
                }
                else
                {
                    targetErea[v].Add((Guid.Empty,CellType.None));
                }
            }
        }

        return targetErea;
    }

    public void Up()
    {
        //最上段に何かしらのピースがある場合は実行しない
        if (Stage[0].Any(value => value.Item2 != CellType.None))
        {
            return;
        }

        //最上段を削除
        Stage.RemoveAt(0);

        //空の行を作成し、最下段に追加
        List<(Guid,CellType)> Row = new List<(Guid,CellType)>();
        for (int rowIndex = 0; rowIndex < stageSize; rowIndex++)
        {
            Row.Add((Guid.Empty,CellType.None));
        }
        Stage.Add(Row);
    }

    public void Down()
    {
        //最下段に何かしらのピースがある場合は実行しない
        if (Stage[stageSize - 1].Any(value => value.Item2 != CellType.None))
        {
            return;
        }

        //最下段を削除
        Stage.RemoveAt(stageSize - 1);

        //空の列を作成し、最上段に追加
        List<(Guid,CellType)> Row = new List<(Guid,CellType)>();
        for (int rowIndex = 0; rowIndex < stageSize; rowIndex++)
        {
            Row.Add((Guid.Empty,CellType.None));
        }
        Stage.Insert(0, Row);
    }

    public void Right()
    {
        //右端列に何かピースがある場合は実行しない。
        for (int ColumnIndex = 0; ColumnIndex < stageSize; ColumnIndex++)
        {
            if (Stage[ColumnIndex][stageSize -1].Item2 != CellType.None)
            {
                return;
            }
        }

        //右端列を削除し、空の列を左列に追加
        for (int ColumnIndex = 0; ColumnIndex < stageSize; ColumnIndex++)
        {
            Stage[ColumnIndex].RemoveAt(stageSize - 1);
            Stage[ColumnIndex].Insert(0, (Guid.Empty,CellType.None));
        }
    }

    public void Left()
    {
        //左端列に何かピースがある場合は実行しない。
        for (int ColumnIndex = 0; ColumnIndex < stageSize; ColumnIndex++)
        {
            if (Stage[ColumnIndex][0].Item2 != CellType.None)
            {
                return;
            }
        }

        //左端列を削除し、空の列を右列に追加
        for (int ColumnIndex = 0; ColumnIndex < stageSize; ColumnIndex++)
        {
            Stage[ColumnIndex].RemoveAt(0);
            Stage[ColumnIndex].Add((Guid.Empty, CellType.None));
        }
    }

}