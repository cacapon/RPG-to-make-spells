using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class StageData : MonoBehaviour
{
    static private int stageSize = 7;
    public List<List<Cell>> Stage;

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
        Stage = new List<List<Cell>>();

        for (int i = 0; i < stageSize; i++)
        {
            Stage.Add(new List<Cell>());
            for (int j = 0; j < stageSize; j++)
            {
                Stage[i].Add(new Cell());
            }
        }
    }

    public void SetStartAndEndCell()
    {
        Stage[0][0].Set(Guid.NewGuid(),"START",CellType.Start,true);
        Stage[stageSize-1][stageSize-1].Set(Guid.NewGuid(),"END",CellType.End,true);
    }
    public string ShowStageData()
    {
        string stagestring = "";
        for (int i = 0; i < Stage.Count; i++)
        {
            for (int j = 0; j < Stage[i].Count; j++)
            {
                stagestring += (int)Stage[i][j].Show();
            }
            stagestring += "\n";
        }
        return stagestring;
    }

    public void SetCell(List<List<Cell>> piecedata, string runeId)
    {
        for (int v = 0; v < piecedata.Count; v++)
        {
            for (int h = 0; h < piecedata[v].Count; h++)
            {
                Stage[v][h].Set(piecedata[v][h].Cellid, runeId, piecedata[v][h].CellType,piecedata[v][h].IsConnectCell);
            }
        }
    }
    public bool isEmpty()
    {
        for (int v = 0; v < Stage.Count; v++)
        {
            for (int h = 0; h < Stage[v].Count; h++)
            {
                if( !Stage[v][h].isEmpty){ return false; }
            }
        }
        return true;
    }

    public bool isEmpty(Vector2Int pos)
    {
        if( !Stage[pos.y][pos.x].isEmpty){ return false; }

        return true;
    }

    public bool isDeplicated(List<List<Cell>> anotherStage)
    {
        //対象のセルがどちらもNone以外のセルの場合Trueとする
        for (int v = 0; v < Stage.Count; v++)
        {
            for (int h = 0; h < Stage[v].Count; h++)
            {
                if( !Stage[v][h].isEmpty &&
                    !anotherStage[v][h].isEmpty)
                    {
                        Debug.Log("Deplicated");
                        return true;
                    }
            }
        }

        return false;
    }

    public void Put(List<List<Cell>> anotherStage)
    {
        for (int v = 0; v < Stage.Count; v++)
        {
            for (int h = 0; h < Stage[v].Count; h++)
            {
                if( !anotherStage[v][h].isEmpty)
                {
                    Stage[v][h] = anotherStage[v][h];
                }
            }
        }
    }

    public List<List<Cell>> Holdon(Vector2Int pos)
    {
        Guid targetGuid = Stage[pos.y][pos.x].Cellid;
        List<List<Cell>> targetErea = new List<List<Cell>>();

        for (int v = 0; v < Stage.Count; v++)
        {
            targetErea.Add(new List<Cell>());
            for (int h = 0; h < Stage[v].Count; h++)
            {
                if( Stage[v][h].Cellid == targetGuid)
                {
                    targetErea[v].Add(Stage[v][h]);
                    Stage[v][h] = new Cell();
                }
                else
                {
                    targetErea[v].Add(new Cell());
                }
            }
        }

        return targetErea;
    }

    public void Up()
    {
        //最上段に何かしらのピースがある場合は実行しない
        if (Stage[0].Any( value => !value.isEmpty )){ return; }

        //最上段を削除
        Stage.RemoveAt(0);

        //空の行を作成し、最下段に追加
        List<Cell> Row = new List<Cell>();
        for (int rowIndex = 0; rowIndex < stageSize; rowIndex++)
        {
            Row.Add(new Cell());
        }
        Stage.Add(Row);
    }

    public void Down()
    {
        //最下段に何かしらのピースがある場合は実行しない
        if (Stage[stageSize - 1].Any(value => !value.isEmpty ))
        {
            return;
        }

        //最下段を削除
        Stage.RemoveAt(stageSize - 1);

        //空の列を作成し、最上段に追加
        List<Cell> Row = new List<Cell>();
        for (int rowIndex = 0; rowIndex < stageSize; rowIndex++)
        {
            Row.Add(new Cell());
        }
        Stage.Insert(0, Row);
    }

    public void Right()
    {
        //右端列に何かピースがある場合は実行しない。
        for (int ColumnIndex = 0; ColumnIndex < stageSize; ColumnIndex++)
        {
            if (!Stage[ColumnIndex][stageSize -1].isEmpty)
            {
                return;
            }
        }

        //右端列を削除し、空の列を左列に追加
        for (int ColumnIndex = 0; ColumnIndex < stageSize; ColumnIndex++)
        {
            Stage[ColumnIndex].RemoveAt(stageSize - 1);
            Stage[ColumnIndex].Insert(0, new Cell());
        }
    }

    public void Left()
    {
        //左端列に何かピースがある場合は実行しない。
        for (int ColumnIndex = 0; ColumnIndex < stageSize; ColumnIndex++)
        {
            if (!Stage[ColumnIndex][0].isEmpty)
            {
                return;
            }
        }

        //左端列を削除し、空の列を右列に追加
        for (int ColumnIndex = 0; ColumnIndex < stageSize; ColumnIndex++)
        {
            Stage[ColumnIndex].RemoveAt(0);
            Stage[ColumnIndex].Add(new Cell());
        }
    }

    public string GetRuneID()
    {
        for (int v = 0; v < Stage.Count; v++)
        {
            if(Stage[v].All(value => value.RuneId == ""))
            {
                continue;
            }
            else
            {
                return Stage[v].Find(value => value.RuneId != "").RuneId;
            }
        }
        return "";
    }
}