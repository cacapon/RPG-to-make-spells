using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetBlockTile : MonoBehaviour
{
    public PlayerData PlayerData;
    public TileBase[] BlockType;

    [SerializeField]
    private Vector3Int Origin = new Vector3Int(7, 12, 0); //FIXME: TileMapの原点位置の調整方法が分からないので、数字で調整しています。


    // Update is called once per frame
    void Update()
    {
        SetBlocks(); // TODO:HPの増減が発生したら呼び出す形にしたい
    }

    private void SetBlocks()
    {
        //HPを10等分し、
        bool[] CurrentHPBlocks = new bool[10];
        bool[] FutureHPBlocks = new bool[10];
        float C_M = PlayerData.CurrentHP / PlayerData.MaxHP;
        float F_M = PlayerData.FutureHP  / PlayerData.MaxHP;

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
