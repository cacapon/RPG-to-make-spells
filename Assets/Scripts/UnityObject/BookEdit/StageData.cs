using System.Collections.Generic;
using UnityEngine;

class StageData : MonoBehaviour
{
    static private int stageSize = 7;
    public List<List<CellType>> Stage;

    private void Awake() {
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
}