using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetBlockTile : MonoBehaviour
{
    public TileBase[] BlockType;
    [SerializeField] private Dataset dataset;
    [SerializeField] private Vector3Int Origin = new Vector3Int(7, 12, 0); //FIXME: TileMapの原点位置の調整方法が分からないので、数字で調整しています。


    private Dictionary<eBlockName,TileBase> Blocks;

    public enum eBlockName
    {
        Gray,
        GraytoGreen,
        GraytoRed,
        Green,
        Blue
    }

    private void Awake()
    {
        MakeDict();
    }

    private void MakeDict()
    {
        Blocks = new Dictionary<eBlockName, TileBase>();

        int i = 0;

        foreach (eBlockName key in Enum.GetValues(typeof(eBlockName)))
        {
            Blocks.Add(key, BlockType[i]);
            i++;
        }
    }

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
        float C_M = dataset.CurrentMP / dataset.MaxMP;

        for (int ite = 0; ite < 10; ite++)
        {
            float check = ite / 10f;

            if (check < C_M){ CurrentMPBlocks[ite] = true;  }
            else            { CurrentMPBlocks[ite] = false; }

            if (CurrentMPBlocks[ite])
            {
                this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(ite, -2, 0), Blocks[eBlockName.Blue]);
            }
            else
            {
                this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(ite, -2, 0), Blocks[eBlockName.Gray]);
            }
        }
    }

    private void SetHPBlocks()
    {
        //HPを10等分し、残量に応じてタイルを選択してセットします。
        bool[] CurrentHPBlocks = new bool[10];
        bool[] FutureHPBlocks = new bool[10];
        float C_M = dataset.CurrentHP / dataset.MaxHP;
        float F_M = dataset.FutureHP  / dataset.MaxHP;

        for (int ite = 0; ite < 10; ite++)
        {
            float check = ite / 10f;

            if (check < C_M){ CurrentHPBlocks[ite] = true;  }
            else            { CurrentHPBlocks[ite] = false; }

            if (check < F_M){ FutureHPBlocks[ite]  = true;  }
            else            { FutureHPBlocks[ite]  = false; }


            if (CurrentHPBlocks[ite] && FutureHPBlocks[ite])
            {
                this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(ite, 0, 0), Blocks[eBlockName.Green]);
            }
            else if (!CurrentHPBlocks[ite] && FutureHPBlocks[ite])
            {
                this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(ite, 0, 0), Blocks[eBlockName.GraytoGreen]);
            }
            else if (CurrentHPBlocks[ite] && !FutureHPBlocks[ite])
            {
                this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(ite, 0, 0), Blocks[eBlockName.GraytoRed]);
            }
            else if (!CurrentHPBlocks[ite] && !FutureHPBlocks[ite])
            {
                this.GetComponent<Tilemap>().SetTile(Origin + new Vector3Int(ite, 0, 0), Blocks[eBlockName.Gray]);
            }
            else
            {
                Debug.LogError("想定外のエラー");
            }

        }
    }

}
