using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class ConnectTileData : MonoBehaviour
{
    public enum eVector
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
    static private int stageSize = 7;
    public List<List<eVector>> TileData;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        TileData = new List<List<eVector>>();

        for (int i = 0; i < stageSize; i++)
        {
            TileData.Add(new List<eVector>());
            for (int j = 0; j < stageSize; j++)
            {
                TileData[i].Add(eVector.None);
            }
        }
    }

    public string Show()
    {
        string TileDataStr = "";
        for (int i = 0; i < TileData.Count; i++)
        {
            for (int j = 0; j < TileData[i].Count; j++)
            {
                TileDataStr += (int)TileData[i][j];
            }
            TileDataStr += "\n";
        }
        return TileDataStr;
    }

    public void SetConnectCell(Vector2Int pos1, Vector2Int pos2)
    {
        Vector2Int vec = pos1 - pos2;

        if(vec.x == -1)
        {
            //pos1-><-pos2
            TileData[pos1.y][pos1.x] = eVector.Right;
            TileData[pos2.y][pos2.x] = eVector.Left;
        }
        else if(vec.x == 1)
        {
            //pos2-><-pos1
            TileData[pos1.y][pos1.x] = eVector.Left;
            TileData[pos2.y][pos2.x] = eVector.Right;
        }
        else if(vec.y == -1)
        {
            //pos1
            //↓
            //↑
            //pos2
            TileData[pos1.y][pos1.x] = eVector.Down;
            TileData[pos2.y][pos2.x] = eVector.Up;
        }
        else if(vec.y == 1)
        {
            //pos2
            //↓
            //↑
            //pos1
            TileData[pos1.y][pos1.x] = eVector.Up;
            TileData[pos2.y][pos2.x] = eVector.Down;
        }
        else
        {
            throw new Exception("想定外のエラー");
        }
    }

    public void DisConnect(List<List<Cell>> stagedata)
    {
        for (int v = 0; v < stagedata.Count; v++)
        {
            for (int h = 0; h < stagedata[v].Count; h++)
            {
                if(TileData[v][h] == eVector.None)
                {
                    continue;
                }
                if(stagedata[v][h].CellType != CellType.None)
                {
                    eVector vec = TileData[v][h];

                    //接続元を解除
                    TileData[v][h] = eVector.None;

                    //接続先の接続も解除
                    switch(vec)
                    {
                        case eVector.Up:
                            TileData[v - 1][h] = eVector.None;
                            break;
                        case eVector.Down:
                            TileData[v + 1][h] = eVector.None;
                            break;
                        case eVector.Left:
                            TileData[v][h - 1] = eVector.None;
                            break;
                        case eVector.Right:
                            TileData[v][h + 1] = eVector.None;
                            break;
                        case eVector.None:
                            throw new Exception("想定外のエラー");
                        default:
                            throw new Exception("想定外のエラー");
                    }
                }
            }
        }
    }

    public void ResetConnectCell(Vector2Int pos)
    {
        TileData[pos.y][pos.x] = eVector.None;
    }
}