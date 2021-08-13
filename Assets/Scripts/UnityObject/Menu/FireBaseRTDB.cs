using UnityEngine;
using Firebase.Database;
using System.Threading.Tasks;

public class FireBaseRTDB : MonoBehaviour
{
    private DatabaseReference reference;

    private string PlayerID;

    public DatabaseReference Reference { get => reference; }

    void Awake()
    {
        PlayerID = PlayerPrefs.GetString(PlayerID);
        //Firebaseのデータベースにアクセスするために必要な初期化処理
        reference = FirebaseDatabase.DefaultInstance.RootReference;
     }

    public async Task<string> GetPieceDataToJson(string PieceID)
    {
        var GetValue = await Reference.Child("PieceDB").Child(PieceID).GetValueAsync();
        return GetValue.GetRawJsonValue();
    }
    public async Task<string> GetRuneDataToJson(string RuneID)
    {
        var GetValue = await Reference.Child("RuneDB").Child(RuneID).GetValueAsync();
        return GetValue.GetRawJsonValue();
    }

}