using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetBlockTile : MonoBehaviour
{
    [SerializeField]
    private PlayerMng PMng;
    public TileBase[] BlockType;

    [SerializeField]
    private Vector3Int Origin = new Vector3Int(7, 12, 0); //FIXME: TileMapの原点位置の調整方法が分からないので、数字で調整しています。


    // Update is called once per frame
    void Update()
    {
        SetHPBlocks(); // TODO:HPの増減が発生したら呼び出す形にしたい
        SetMPBlocks();
    }

    private void SetMPBlocks()
    {
        //MPを10等分し、残量に応じてタイルを選択してセットします。
        //TODO:消費量に応じた対応は未実装　現状MPの有無のみでタイルをセットしています。

        bool[] CurrentMPBlocks = new bool[10];
        float C_M = PMng.PData.CurrentMP / PMng.PData.MaxMP;

        for (int ite = 0; ite < 10; ite++)
        {
            float check = ite / 10f;

            if (check < C_M){ CurrentMPBlocks[ite] = true;  }
            else            { CurrentMPBlocks[ite] = false; }

            if (CurrentMPBlocks[ite])
            {
                this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(ite, -2, 0), BlockType[4]);
            }
            else
            {
                this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(ite, -2, 0), BlockType[0]);
            }
        }
    }

    private void SetHPBlocks()
    {
        //HPを10等分し、残量に応じてタイルを選択してセットします。
        bool[] CurrentHPBlocks = new bool[10];
        bool[] FutureHPBlocks = new bool[10];
        float C_M = PMng.PData.CurrentHP / PMng.PData.MaxHP;
        float F_M = PMng.PData.FutureHP  / PMng.PData.MaxHP;

        for (int ite = 0; ite < 10; ite++)
        {
            float check = ite / 10f;

            if (check < C_M){ CurrentHPBlocks[ite] = true;  }
            else            { CurrentHPBlocks[ite] = false; }

            if (check < F_M){ FutureHPBlocks[ite]  = true;  }
            else            { FutureHPBlocks[ite]  = false; }


            if (CurrentHPBlocks[ite] && FutureHPBlocks[ite])
            {
                this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(ite, 0, 0), BlockType[3]);
            }
            else if (!CurrentHPBlocks[ite] && FutureHPBlocks[ite])
            {
                this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(ite, 0, 0), BlockType[2]);
            }
            else if (CurrentHPBlocks[ite] && !FutureHPBlocks[ite])
            {
                this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(ite, 0, 0), BlockType[1]);
            }
            else if (!CurrentHPBlocks[ite] && !FutureHPBlocks[ite])
            {
                this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(ite, 0, 0), BlockType[0]);
            }
            else
            {
                Debug.LogError("想定外のエラー");
            }

        }
    }

}
