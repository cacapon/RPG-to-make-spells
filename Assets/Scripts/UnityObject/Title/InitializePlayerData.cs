using System;
using UnityEngine;
using Firebase.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

public class InitializePlayerData : MonoBehaviour
{
    DatabaseReference reference;
    private string PlayerID = "PlayerID";


    void Awake()
    {
        //Firebaseのデータベースにアクセスするために必要な初期化処理
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void Exec()
    {
        if (!PlayerPrefs.HasKey(PlayerID))
        {
            ToLocal();
            SaveInitPlayerData();
        }
    }

    /// <summary>
    /// 初回のみ、ローカルに一意なIDを保存します。
    /// </summary>
    private void ToLocal()
    {
        //ローカルに一意なIDを保存
        PlayerPrefs.SetString(PlayerID, Guid.NewGuid().ToString());
        Debug.Log("IDをセーブしました");
    }

    /// <summary>
    /// 初期プレイヤーデータをFirebaseに保存します
    /// </summary>
    /// <returns></returns>
    private async void SaveInitPlayerData()
    {
        /*
            PlayerDB:
                <ID>:
                    HP:100
                    MP:50
                    Gold:500
                    CurrentStage:0

            InventoryDB:
                <ID>:
                    R_DMG_5_000_000_C:1
        */
        string pid = PlayerPrefs.GetString(PlayerID);
        await reference.Child("PlayerDB").Child(pid).SetValueAsync(new Dictionary<string, int>(){
                    {"HP",100},
                    {"MP",50},
                    {"Gold",500},
                    {"CurrentStage",0}
                });
        await reference.Child("InventoryDB").Child(pid).SetValueAsync(new Dictionary<string, int>()
        {
            {"FIRE_5_000_000_M",1},
            {"ICE_5_000_001_C",1},
            {"THUNDER_5_000_002_Y",1},
            {"FIRE_5_001_003_M",1},
            {"ICE_5_001_004_C",1},
            {"THUNDER_5_001_005_Y",1},
            {"ME_4_000_000_C",1},
            {"ME_4_001_000_M",1},
            {"ME_4_002_000_Y",1},
            {"MULTI_3_000_000_C",1},
            {"MULTI_3_002_000_M",1},
            {"MULTI_3_004_000_Y",1},
            {"REVERCE_5_000_000_C",5},
        });
        Debug.Log("IDをDBへセーブしました");
    }
}