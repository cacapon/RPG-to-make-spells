using UnityEngine;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;

public class LoadPlayerData : MonoBehaviour
{
    [SerializeField] FireBaseRTDB fireBaseRTDB;

    private PlayerData pData;
    private string PlayerID = "PlayerID";

    public PlayerData PData { get => pData; }

    void Awake()
    {
        pData = PlayerData.CreateInstance<PlayerData>();
        Load();
    }

    /// <summary>
    /// DB(Firebase Realtime Database)から一意なIDをロードします
    /// </summary>
    /// <returns></returns>
    private async void Load()
    {
        string pid = PlayerPrefs.GetString(PlayerID);
        var playerData    = await fireBaseRTDB.Reference.Child("PlayerDB").Child(pid).GetValueAsync();
        var inventoryData = await fireBaseRTDB.Reference.Child("InventoryDB").Child(pid).GetValueAsync();

        Dictionary<string, int> pDataDict   = ConvertDict.ValueInt(playerData.Value);
        Dictionary<string, int> invDataDict = ConvertDict.ValueInt(inventoryData.Value);

        pData.InitHP = pDataDict["HP"];
        pData.InitMP = pDataDict["MP"];
        pData.Gold   = pDataDict["Gold"];
        pData.Inventory = invDataDict;
        pData.Book = new List<Magic>();
    }
}