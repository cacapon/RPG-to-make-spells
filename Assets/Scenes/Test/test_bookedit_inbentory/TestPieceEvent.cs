using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestPieceEvent : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private TestBookEditStageManager testBookEditStageManager;
    private int myIndex;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //番号から(-3,-3)~(3,3)の座標がどこかを取得する
        //取得した座標を現在の中心としてBookEtidStageManagerに返す
        testBookEditStageManager.MoveHoldStage(GetPos());
    }

    // Start is called before the first frame update
    private void Awake()
    {
        myIndex = Convert.ToInt32(Regex.Match(gameObject.name, @"\d+").Value);
    }

    private Vector2Int GetPos()
    {
        int size = testBookEditStageManager.GetStageSize;
        return new Vector2Int(myIndex % size , myIndex / size);
    }
}
