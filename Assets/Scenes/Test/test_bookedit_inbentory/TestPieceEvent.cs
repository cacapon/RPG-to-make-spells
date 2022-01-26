using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestPieceEvent : MonoBehaviour, IPointerEnterHandler,IPointerClickHandler
{
    public TestBookEditSceneData data;
    [SerializeField] private TestBookEditStageManager testBookEditStageManager;
    private int myIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(data.HoldParts.IsActive)
        {
            //持ち上げ中の場合
            testBookEditStageManager.PutHoldParts();
        }
        else
        {
            //持ち上げ中ではない場合
            testBookEditStageManager.HoldUpParts(GetPos());
        }
    }

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
        int size = data.StageSize;
        return new Vector2Int(myIndex % size , myIndex / size);
    }
}
