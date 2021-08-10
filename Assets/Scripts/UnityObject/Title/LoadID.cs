using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class LoadID : MonoBehaviour
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
        if (PlayerPrefs.HasKey(PlayerID))
        {
            ToDB();
        }
    }

    /// <summary>
    /// DB(Firebase Realtime Database)から一意なIDをロードします
    /// </summary>
    /// <returns></returns>
    private async void ToDB()
    {
        if (PlayerPrefs.HasKey(PlayerID))
        {
            string value = PlayerPrefs.GetString(PlayerID);
            var Id = await reference.Child(PlayerID).GetValueAsync();
            Debug.Log("DBからIDをロードしました");
            Debug.Log(Id);
        }
        else
        {
            Debug.LogError("IDが存在しません");
        }
    }
}
