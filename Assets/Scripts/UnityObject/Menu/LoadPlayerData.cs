using UnityEngine;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;

public class LoadPlayerData : MonoBehaviour
{
    DatabaseReference reference;

    private PlayerData pData;
    private string PlayerID = "PlayerID";

    public PlayerData PData { get => pData; }

    void Awake()
    {
        //Firebaseのデータベースにアクセスするために必要な初期化処理
        reference = FirebaseDatabase.DefaultInstance.RootReference;
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
        var playerData    = await reference.Child("PlayerDB").Child(pid).GetValueAsync();
        var inventoryData = await reference.Child("InventoryDB").Child(pid).GetValueAsync();

        Dictionary<string, int> pDataDict   = ConvertDict(playerData.Value);
        Dictionary<string, int> invDataDict = ConvertDict(inventoryData.Value);

        pData.InitHP = pDataDict["HP"];
        pData.InitMP = pDataDict["MP"];
        pData.Gold   = pDataDict["Gold"];
        pData.Inventory = invDataDict;
        pData.Book = new List<Magic>();
    }

    private Dictionary<string,int> ConvertDict(object value)
    {
        Dictionary<string, int> returnDict = (value as Dictionary<string, object>).ToDictionary(k => k.Key, k => Convert.ToInt32(k.Value));
        return returnDict;
    }

}