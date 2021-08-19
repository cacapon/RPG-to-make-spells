using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PieceCommand : MonoBehaviour
{
    [SerializeField] private BookEditSetRune bookEditSetRune;
    [SerializeField] private PlayerData PData;
    [SerializeField] private StageTile stageTile;
    [SerializeField] private StageTile HoldTile;
    [SerializeField] private StageData Hold;
    [SerializeField] private StageData Stage;

    [SerializeField] private GameObject HoldTapPanel;
    [SerializeField] private GameObject StageTapPanel;

    enum eMoveVector
    {
        Up,
        Down,
        Left,
        Right
    }

    public void BringFromInventory(Rune rune, Piece piece)
    {
        if (!Hold.isEmpty()){ return; }
        if (PData.Inventory[rune.RuneID] == 0){ return; }

        PData.Inventory[rune.RuneID] -= 1;
        List<List<CellType>> piecedata = MakePiece(piece);
        Hold.SetCell(piecedata);

        HoldTile.SetStageTile();
        bookEditSetRune.ReLoad();
        SwitchTapPanel(true);
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

        foreach (Vector2Int pos in piece.Shape)
        {
            piecedata[pos.y][pos.x] = (CellType)piece.Color;
        }

        return piecedata;
    }

    private void SwitchTapPanel(bool enableHold)
    {
        if(enableHold)
        {
            HoldTapPanel.SetActive(true);
            StageTapPanel.SetActive(false);
        }
        else
        {
            HoldTapPanel.SetActive(false);
            StageTapPanel.SetActive(true);
        }
    }

    public void Put()
    {
        if (Hold.isDeplicated(Stage.Stage)){return;}
        Stage.Put(Hold.Stage);
        stageTile.SetStageTile();
        Hold.Init();
        HoldTile.SetStageTile();
        SwitchTapPanel(false);
    }

    public void Holdon(Vector2Int targetpos)
    {
        if (!Hold.isEmpty()){return;}

        //指定のステージのブロックをホールドに移す
        Hold.Stage = Stage.Holdon(targetpos);
        stageTile.SetStageTile();
        HoldTile.SetStageTile();
        SwitchTapPanel(true);
    }

    private void Move(eMoveVector vec)
    {
        //Down
        //Holdの上段に一つ追加
        //Holdの下段を一つ削除
        switch (vec)
        {
            case eMoveVector.Up:
                Hold.Up();
                break;
            case eMoveVector.Down:
                Hold.Down();
                break;
            case eMoveVector.Right:
                Hold.Right();
                break;
            case eMoveVector.Left:
                Hold.Left();
                break;
            default:
                break;
        }

        HoldTile.SetStageTile();

    }

    public void MoveUp()
    {
        Move(eMoveVector.Up);
    }
    public void MoveDown()
    {
        Move(eMoveVector.Down);
    }
    public void MoveLeft()
    {
        Move(eMoveVector.Left);
    }
    public void MoveRight()
    {
        Move(eMoveVector.Right);
    }
}
