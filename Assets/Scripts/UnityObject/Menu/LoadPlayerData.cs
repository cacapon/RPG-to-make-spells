using UnityEngine;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;

public class LoadPlayerData : MonoBehaviour
{
    [SerializeField] FireBaseRTDB fireBaseRTDB;

    public PlayerData PData;
    private string PlayerID = "PlayerID";

    void Awake()
    {
        //PData = PlayerData.CreateInstance<PlayerData>();
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

        PData.InitHP = pDataDict["HP"];
        PData.InitMP = pDataDict["MP"];
        PData.Gold   = pDataDict["Gold"];
        PData.Inventory = invDataDict;
        PData.Book = new List<Magic>();
    }
}