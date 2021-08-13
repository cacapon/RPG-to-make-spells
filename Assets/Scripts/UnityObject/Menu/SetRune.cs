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
    [SerializeField] LoadPlayerData loadPlayerData;
    [SerializeField] FireBaseRTDB fireBaseRTDB;

    private Image targetBaseImage;
    private RectTransform targetSocketRect;
    private RectTransform targetPlugRect;
    private Text targetCount;

    private DataSnapshot RuneData;
    private DataSnapshot PieceData;

    void Start()
    {
        foreach (string RuneID in loadPlayerData.PData.Inventory.Keys)
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
        SetCount(rune, loadPlayerData.PData.Inventory[runeID]);

    }

    private void SetSocketandPlug(Piece pieceInstance, GameObject rune)
    {
        Vector2 basePivot = new Vector2(0, 1);

        targetSocketRect = rune.transform.GetChild(1).GetComponent<RectTransform>();
        targetSocketRect.pivot = basePivot + pieceInstance.Socket;

        targetPlugRect = rune.transform.GetChild(2).GetComponent<RectTransform>();
        targetPlugRect.pivot = basePivot + pieceInstance.Plug;
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

}

[Serializable]
public class Rune
{
    public string PieceID;
    public string Type;
}

[Serializable]
public class Piece
{
    public int Color;
    public Vector2Int Plug;
    public Vector2Int Socket;
    public Vector2Int ShapeSize;
    public List<Vector2Int> Shape;
}

