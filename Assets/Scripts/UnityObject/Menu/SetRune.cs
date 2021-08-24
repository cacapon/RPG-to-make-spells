using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SetRune : MonoBehaviour
{
    [SerializeField] GameObject RunePrefub;
    [SerializeField] PlayerData PData;
    [SerializeField] FireBaseRTDB fireBaseRTDB;

    private Image targetBaseImage;
    private RectTransform targetSocketRect;
    private RectTransform targetPlugRect;
    private Text targetCount;

    private DataSnapshot RuneData;
    private DataSnapshot PieceData;

    void Start()
    {
        foreach (string RuneID in PData.Inventory.Keys)
        {
            SetInventoryData(RuneID);
        }
    }
    private async void SetInventoryData(string runeID)
    {
        string runejson = await fireBaseRTDB.GetRuneDataToJson(runeID);
        Rune runeInstance = JsonUtility.FromJson<Rune>(runejson);

        string piecejson = await fireBaseRTDB.GetPieceDataToJson(runeInstance.PieceID);
        Piece pieceInstance = JsonUtility.FromJson<Piece>(piecejson);

        GameObject rune = Instantiate(RunePrefub);

        rune.transform.SetParent(gameObject.transform);
        rune.transform.localPosition = Vector3.zero;
        rune.transform.localScale = Vector3.one;

        SetBaseImage(rune, runeInstance.PieceID);
        SetSocketandPlug(pieceInstance, rune);
        SetCount(rune, PData.Inventory[runeID]);

    }

    private void SetSocketandPlug(Piece pieceInstance, GameObject rune)
    {
        Vector2 basePivot = new Vector2(0, 1);

        Vector2 Socket = new Vector2(pieceInstance.Socket.x * -1, pieceInstance.Socket.y);
        Vector2 Plug = new Vector2(pieceInstance.Plug.x * -1, pieceInstance.Plug.y);

        targetSocketRect = rune.transform.GetChild(1).GetComponent<RectTransform>();
        targetSocketRect.pivot = basePivot + Socket;

        targetPlugRect = rune.transform.GetChild(2).GetComponent<RectTransform>();
        targetPlugRect.pivot = basePivot + Plug;
    }

    private void SetBaseImage(GameObject rune, string pieceID)
    {
        targetBaseImage = rune.transform.GetChild(0).GetComponent<Image>();

        string name = Regex.Replace(pieceID, "([1-5])_([0-9]..)_([0-9]..)_([C,M,Y])", "$1_$2_$4"); //ex: 5_001_002_C -> 5_001_C

        targetBaseImage.sprite = Resources.Load<Sprite>($"Inventory/{name}");
    }
    private void SetCount(GameObject rune, int count)
    {
        targetCount = rune.transform.GetChild(3).GetComponent<Text>();

        targetCount.text = count.ToString();
    }

    public void ReLoad()
    {
        foreach (Transform child in gameObject.transform)
        {
            //削除する
            Destroy(child.gameObject);
        }
        Start();
    }

}