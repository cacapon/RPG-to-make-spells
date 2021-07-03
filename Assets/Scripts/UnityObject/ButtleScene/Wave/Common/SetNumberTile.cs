using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetNumberTile : MonoBehaviour
{
    [SerializeField]
    private PlayerMng PMng;
    public TileBase[] Numbers;

    [SerializeField]
    private Vector3Int Origin = new Vector3Int(-7,13,0); //FIXME: TileMapの原点位置の調整方法が分からないので、数字で調整しています。

    // Update is called once per frame
    void Update()
    {
        SetCurrentHPTile();
        SetMaxHPTile();
        SetCurrentMPTile();
        SetMaxMPTile();
    }

    private void SetCurrentHPTile()
    {
        SetNumTile(1,0,((int)PMng.PData.CurrentHP));
    }

    private void SetMaxHPTile()
    {
        SetNumTile(6,0, ((int)PMng.PData.MaxHP));
    }

    private void SetCurrentMPTile()
    {
        SetNumTile(1, -2, ((int)PMng.PData.CurrentMP));
    }
    private void SetMaxMPTile()
    {
        SetNumTile(6, -2, ((int)PMng.PData.MaxMP));
    }



    private void SetNumTile(int startXPosition,int startYPosition, int hp)
    {
        /*
            [イメージ]
                O:icon X:CurrentHP Y:MaxHP
                index   0123456789
                        OXXXX/YYYY
            HPを配列化したのちに、対象のインデックスに数字タイルを当てはめる処理を行います。
        */
        char[] chnums = hp.ToString().ToCharArray();

        //初期化
        for(int ite=0; ite<4; ite++)
        {
            this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(startXPosition + ite, startYPosition, 0), null);
        }

        if (1 <= chnums.Length && chnums.Length <= 4)
        {
            for (int index = 0; index < chnums.Length; index++)
            {
                int XPosition = startXPosition + 4 - chnums.Length + index;
                this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(XPosition, startYPosition, 0), Numbers[(int)Char.GetNumericValue(chnums[index])]);
            }
        }
    }
}
