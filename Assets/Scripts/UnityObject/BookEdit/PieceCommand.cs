using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PieceCommand : MonoBehaviour
{
    [SerializeField] private PlayerData PData;
    [SerializeField] private StageTile stageTile;
    [SerializeField] private StageTile HoldTile;

    [SerializeField] private StageData Hold;

    public void BringFromInventory(Rune rune, Piece piece)
    {
        if(PData.Inventory[rune.RuneID] == 0)
        {
            return;
        }
        Debug.Log($"{rune.RuneID}:{PData.Inventory[rune.RuneID]}");
        PData.Inventory[rune.RuneID] -= 1;
        Debug.Log($"{rune.RuneID}:{PData.Inventory[rune.RuneID]}");
        List<List<CellType>> piecedata = MakePiece(piece);
        Hold.SetCell(piecedata);
        Debug.Log(Hold.ShowStageData());

        HoldTile.SetStageTile();
    }

    private List<List<CellType>> MakePiece(Piece piece)
    {
        List<List<CellType>> piecedata = new List<List<CellType>>();

        for (int i = 0; i < piece.ShapeSize.y; i++)
        {
            piecedata.Add(new List<CellType>());
            for (int j = 0; j < piece.ShapeSize.x; j++)
            {
                piecedata[i].Add(CellType.None);
            }
        }

        foreach(Vector2Int pos in piece.Shape)
        {
            piecedata[pos.y][pos.x] = (CellType)piece.Color;
        }

        return piecedata;
    }

}
