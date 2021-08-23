using System;
using System.Collections.Generic;
using UnityEngine;

class ConnectList : MonoBehaviour
{
    public List<Connect> CList;

    private void Awake()
    {
        CList = new List<Connect>();
    }

    public void Add(string aRuneID, string bRuneID)
    {
        CList.Add(new Connect());
        CList[CList.Count -1].Set(aRuneID, bRuneID);
    }

    public string Show()
    {
        string cListStr = "";
        foreach(Connect c in CList)
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

    public Connect()
    {
        aRuneID = null;
        bRuneID = null;
    }

    public string ARuneID { get => aRuneID;}
    public string BRuneID { get => bRuneID;}

    public void Set(string aRuneID, string bRuneID)
    {
        this.aRuneID = aRuneID;
        this.bRuneID = bRuneID;
    }

}