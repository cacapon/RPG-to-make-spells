using System;
using System.Collections.Generic;
using UnityEngine;

class ConnectList : MonoBehaviour
{
    public List<Connect> CList;

    private void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        CList = new List<Connect>();
    }

    public bool ChainCheck()
    {
        //STARTで始まりENDで終わるかチェック
        if(CList.Count == 0){ return false;}
        int checklimit = 20;

        Guid next = CList.Find(connect => connect.ARuneID == "START").BInstantID;
        if(CList.Find(connect => connect.BInstantID == next).BRuneID == "END")
        {
            return true;
        }

        for (int i = 0; i < checklimit; i++)
        {
            if(CList.Find(connect => connect.BInstantID == next).BRuneID == "END")
            {
                return true;
            }
            next = CList.Find(connect => connect.AInstantID == next).BInstantID;
        }

        return false;
    }

    private bool Contains(Guid aInstantID, Guid bInstantID)
    {
        //aとbを逆にしたListが既にあるのなら重複有りとみなす
        foreach (var item in CList)
        {
            if( aInstantID == item.AInstantID &&
                bInstantID == item.BInstantID)
            {
                return true;
            }
        }
        return false;
    }

    private void Add(string aRuneID, string bRuneID, Guid aInstantID, Guid bInstantID)
    {
        //重複チェック
        if(Contains(bInstantID,aInstantID)){ return; }

        CList.Add(new Connect());
        CList[CList.Count - 1].Set(aRuneID, bRuneID, aInstantID, bInstantID);
    }

    public void SearchConnect(StageData stage, ConnectTileData connectTileData)
    {
        for (int i = 0; i < connectTileData.TileData.Count; i++)
        {
            for (int j = 0; j < connectTileData.TileData[i].Count; j++)
            {
                if (connectTileData.TileData[i][j] != ConnectTileData.eVector.None)
                {
                    switch (connectTileData.TileData[i][j])
                    {
                        case ConnectTileData.eVector.Up:
                            Add(
                                stage.Stage[i][j].RuneId,
                                stage.Stage[i - 1][j].RuneId,
                                stage.Stage[i][j].Cellid,
                                stage.Stage[i - 1][j].Cellid
                            );
                            break;
                        case ConnectTileData.eVector.Down:
                            Add(
                                stage.Stage[i][j].RuneId,
                                stage.Stage[i + 1][j].RuneId,
                                stage.Stage[i][j].Cellid,
                                stage.Stage[i + 1][j].Cellid
                            );
                            break;
                        case ConnectTileData.eVector.Left:
                            Add(
                                stage.Stage[i][j].RuneId,
                                stage.Stage[i][j - 1].RuneId,
                                stage.Stage[i][j].Cellid,
                                stage.Stage[i][j - 1].Cellid
                            );
                            break;
                        case ConnectTileData.eVector.Right:
                            Add(
                                stage.Stage[i][j].RuneId,
                                stage.Stage[i][j + 1].RuneId,
                                stage.Stage[i][j].Cellid,
                                stage.Stage[i][j + 1].Cellid
                            );
                            break;
                        case ConnectTileData.eVector.None:
                            throw new Exception("想定外のエラー");
                        default:
                            throw new Exception("想定外のエラー");
                    }
                }
            }
        }
    }

    public string Show()
    {
        string cListStr = "";
        foreach (Connect c in CList)
        {
            cListStr += $"{c.ARuneID}--{c.BRuneID}\n";
        }
        return cListStr;
    }
}

class Connect
{
    string aRuneID;
    string bRuneID;
    Guid aInstantID;
    Guid bInstantID;

    public Connect()
    {
        aRuneID = null;
        bRuneID = null;
        aInstantID = Guid.Empty;
        bInstantID = Guid.Empty;
    }

    public Connect(string aRuneID, string bRuneID, Guid aInstantID, Guid bInstantID)
    {
        this.aRuneID = aRuneID;
        this.bRuneID = bRuneID;
        this.aInstantID = aInstantID;
        this.bInstantID = bInstantID;
    }

    public string ARuneID { get => aRuneID; }
    public string BRuneID { get => bRuneID; }
    public Guid AInstantID { get => aInstantID;}
    public Guid BInstantID { get => bInstantID;}

    public void Set(string aRuneID, string bRuneID, Guid aInstantID, Guid bInstantID)
    {
        this.aRuneID = aRuneID;
        this.bRuneID = bRuneID;
        this.aInstantID = aInstantID;
        this.bInstantID = bInstantID;
    }

}