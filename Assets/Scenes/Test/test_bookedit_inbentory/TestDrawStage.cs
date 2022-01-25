using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TestDrawStage : MonoBehaviour
{
    [SerializeField] private TestBookEditStageManager testBookEditStageManager;
    [SerializeField] private float blinkSpeed = 2.5f;
    private Sprite[] tilePallets;
    // Start is called before the first frame update
    private Image[] stageTiles;

    private float alphaSin;
    private float counttime;

    void Start()
    {
        tilePallets = Resources.LoadAll<Sprite>("TestData/test_bookedit_inbentory/Tiles16x16");

        stageTiles = this.gameObject.GetComponentsInChildren<Image>(); //1次元になるので扱い方に注意
        stageTiles = stageTiles.Where((source, index) =>index != 0).ToArray(); //先頭のデータは親で使用しないので削除する

        foreach(var item in tilePallets)
        {
            Debug.Log(item.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        counttime += Time.deltaTime * blinkSpeed;
        DrawTiles();
    }

    private void DrawTiles()
    {
        var holdStage = testBookEditStageManager.GetStage(true);
        var groundStage = testBookEditStageManager.GetStage(false);

        // 持ち上げと設置ステージを交互に描画する　→　点滅
        for (int height = 0; height < testBookEditStageManager.GetStageSize; height++)
        {
            for (int width = 0; width < testBookEditStageManager.GetStageSize; width++)
            {
                int stageIndex = height * testBookEditStageManager.GetStageSize + width;

                if (holdStage[height, width].MyTile != eTile.None)
                {
                    //点滅して描く
                    DrawBlinkingTile(stageIndex, holdStage[height,width].MyTile ,groundStage[height,width].MyTile);
                }
                else
                {
                    //設置済みのタイルだけ描く
                    stageTiles[stageIndex].sprite = tilePallets[(int)groundStage[height, width].MyTile];
                }
            }
        }
    }

    private void DrawBlinkingTile(int stageIndex, eTile holdTile, eTile groundTile)
    {
        if ( Math.Floor(counttime)  % 2 == 0)
        {
            stageTiles[stageIndex].sprite = tilePallets[(int)holdTile];
        }
        else
        {
            stageTiles[stageIndex].sprite = tilePallets[(int)groundTile];
        }
    }
}
