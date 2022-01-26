using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TestDrawStage : MonoBehaviour
{
    [SerializeField] private TestBookEditSceneData data;
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
        stageTiles = stageTiles.Where((source, index) => index != 0).ToArray(); //先頭のデータは親で使用しないので削除する

        foreach (var item in tilePallets)
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
        // 持ち上げと設置ステージを交互に描画する　→　点滅
        for (int height = 0; height < data.StageSize; height++)
        {
            for (int width = 0; width < data.StageSize; width++)
            {
                int stageIndex = height * data.StageSize + width;

                if (data.HoldStage.GetTile(height,width).MyTile != eTile.None)
                {
                    //点滅して描く
                    DrawBlinkingTile(stageIndex, data.HoldStage.GetTile(height,width).MyTile, data.GroundStage.GetTile(height,width).MyTile);
                }
                else
                {
                    //設置済みのタイルだけ描く
                    stageTiles[stageIndex].sprite = tilePallets[(int)data.GroundStage.GetTile(height,width).MyTile];
                }
            }
        }
    }

    private void DrawBlinkingTile(int stageIndex, eTile holdTile, eTile groundTile)
    {
        if (Math.Floor(counttime) % 2 == 0)
        {
            stageTiles[stageIndex].sprite = tilePallets[(int)holdTile];
        }
        else
        {
            stageTiles[stageIndex].sprite = tilePallets[(int)groundTile];
        }
    }
}
