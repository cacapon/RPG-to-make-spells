using System;
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
    [SerializeField] private ConnectTile connectTile;

    [SerializeField] private StageData Hold;
    [SerializeField] private StageData Stage;
    [SerializeField] private ConnectTileData ConnectTileData;

    [SerializeField] private GameObject HoldTapPanel;
    [SerializeField] private GameObject StageTapPanel;
    [SerializeField] private GameObject CellCommand;
    [SerializeField] private GameObject ConnectButton;

    [SerializeField] private GameObject ConnectCommand;
    [SerializeField] private ConnectList connectList;

    private Vector2Int CellCommandPos;

    enum eVector
    {
        Up,
        Down,
        Left,
        Right
    }

    public void Magicle()
    {
        //StageのinstanceID、RuneIDと、接続部分から接続しているピースのリストを取得
        connectList.Reset();
        connectList.SearchConnect(Stage,ConnectTileData);

        if(connectList.ChainCheck())
        {
            List<Magic> Book = new List<Magic>();
            Magic2 magic = new Magic2();
            foreach (var chain in connectList.CList)
            {
                //magic.ConvertMagic();
            }
        }

        //コネクトのリスト一覧から
        Debug.Log(connectList.Show());
    }

    public void BringFromInventory(Rune rune, Piece piece)
    {
        if (!Hold.isEmpty()){ return; }
        if (PData.Inventory[rune.RuneID] == 0){ return; }

        CellCommand.SetActive(false);
        ConnectCommand.SetActive(false);


        PData.Inventory[rune.RuneID] -= 1;
        List<List<Cell>> piecedata = MakePiece(piece,rune.RuneID);
        Hold.SetCell(piecedata,rune.RuneID);

        HoldTile.SetStageTile();
        bookEditSetRune.ReLoad();
        SwitchTapPanel(true);
    }

    public void PutInInventory()
    {
        Debug.Log("run put in inventory");
        string targetRuneid = Hold.GetRuneID();
        if(targetRuneid == "START" || targetRuneid == "END"){return;} //検知したピースがスタートかENDの場合は実行しない


        Hold.Init();
        HoldTile.SetStageTile();
        PData.Inventory[targetRuneid] += 1;
        bookEditSetRune.ReLoad();
        SwitchTapPanel(false);
    }

    public void PutInInventoryFromCommand()
    {
        if( Stage.Stage[CellCommandPos.y][CellCommandPos.x].RuneId == "START" ||
            Stage.Stage[CellCommandPos.y][CellCommandPos.x].RuneId == "END")
        {
            return;
        }

        Holdon(CellCommandPos);
        PutInInventory();
        CellCommand.SetActive(false);
    }

    private List<List<Cell>> MakePiece(Piece piece, string runeid)
    {
        List<List<Cell>> piecedata = new List<List<Cell>>();
        Guid instantId = Guid.NewGuid();

        //ピースのサイズ領域の準備
        for (int i = 0; i < piece.ShapeSize.y; i++)
        {
            piecedata.Add(new List<Cell>());
            for (int j = 0; j < piece.ShapeSize.x; j++)
            {
                piecedata[i].Add(new Cell());
            }
        }

        //色を付ける
        foreach (Vector2Int pos in piece.Shape)
        {
            piecedata[pos.y][pos.x].Set(instantId,runeid,(CellType)piece.Color);
        }

        //接続部分の色を変える
        switch((CellType)piece.Color)
        {
            case CellType.Cyan:
                piecedata[piece.Plug.y][piece.Plug.x].Set(instantId,runeid,CellType.CyanConnect,true);
                piecedata[piece.Socket.y][piece.Socket.x].Set(instantId,runeid,CellType.CyanConnect,true);
                break;
            case CellType.Magenta:
                piecedata[piece.Plug.y][piece.Plug.x].Set(instantId,runeid,CellType.MagentaConnect,true);
                piecedata[piece.Socket.y][piece.Socket.x].Set(instantId,runeid,CellType.MagentaConnect,true);
                break;
            case CellType.Yellow:
                piecedata[piece.Plug.y][piece.Plug.x].Set(instantId,runeid,CellType.YellowConnect,true);
                piecedata[piece.Socket.y][piece.Socket.x].Set(instantId,runeid,CellType.YellowConnect,true);
                break;
            default:
                throw new Exception($"想定外の色が選択されました:{piece.Color}");
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

    public void ShowCellCommand(Vector2Int pos)
    {
        if (Stage.isEmpty(pos)){return;}

        HideConnectCommand();

        //位置調整
        CellCommand.SetActive(true);
        ConnectButton.SetActive(Stage.Stage[pos.y][pos.x].IsConnectCell);

        RectTransform rectTransform = CellCommand.GetComponent<RectTransform>();
        rectTransform.anchoredPosition =  new Vector2(pos.x*16,-pos.y*16);
        CellCommandPos = pos;
    }

    public void HideCellCommand()
    {
        CellCommand.SetActive(false);
    }

    public void HideConnectCommand()
    {
        ConnectCommand.SetActive(false);
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

    public void ShowConnectCommand()
    {
        CellCommand.SetActive(false);
        ConnectCommand.SetActive(true);
        RectTransform rectTransform = ConnectCommand.GetComponent<RectTransform>();
        rectTransform.anchoredPosition =  new Vector2(CellCommandPos.x*16,-CellCommandPos.y*16);

    }

    public void Holdon()
    {
        //CellCommandからの実行用
        HoldonCommon(CellCommandPos);

        CellCommand.SetActive(false);
    }

    public void Holdon(Vector2Int targetpos)
    {
        HoldonCommon(targetpos);
    }

    private void HoldonCommon(Vector2Int pos)
    {
        if (!Hold.isEmpty()){return;}
        if (Stage.isEmpty(pos)){return;}

        //指定のステージのブロックをホールドに移す
        Hold.Stage = Stage.Holdon(pos);
        stageTile.SetStageTile();
        HoldTile.SetStageTile();
        ConnectTileData.DisConnect(Hold.Stage);
        connectTile.SetConnectTile();
        SwitchTapPanel(true);

    }

    private void Move(eVector vec)
    {
        switch (vec)
        {
            case eVector.Up:
                Hold.Up();
                break;
            case eVector.Down:
                Hold.Down();
                break;
            case eVector.Right:
                Hold.Right();
                break;
            case eVector.Left:
                Hold.Left();
                break;
            default:
                break;
        }

        HoldTile.SetStageTile();

    }

    public void MoveUp()
    {
        Move(eVector.Up);
    }
    public void MoveDown()
    {
        Move(eVector.Down);
    }
    public void MoveLeft()
    {
        Move(eVector.Left);
    }
    public void MoveRight()
    {
        Move(eVector.Right);
    }

    private void Connect(Vector2Int targetpos)
    {
        if(Stage.Stage[targetpos.y][targetpos.x].IsConnectCell)
        {
            ConnectTileData.SetConnectCell(CellCommandPos,targetpos);
            connectTile.SetConnectTile();
        }
        ConnectCommand.SetActive(false);
    }

    public void ConnectUp()
    {
        Vector2Int targetpos = new Vector2Int(CellCommandPos.x, CellCommandPos.y - 1);
        //上限なら何もしない
        if(targetpos.y < 0){ return; }

        Connect(targetpos);
    }
    public void ConnectDown()
    {
        Vector2Int targetpos = new Vector2Int(CellCommandPos.x, CellCommandPos.y + 1);
        //下限なら何もしない
        if(targetpos.y >= Stage.Stage.Count){ return; }

        Connect(targetpos);
    }
    public void ConnectRight()
    {
        Vector2Int targetpos = new Vector2Int(CellCommandPos.x + 1, CellCommandPos.y);
        //左端なら何もしない
        if(targetpos.x >= Stage.Stage.Count){ return; }
        Connect(targetpos);
        Debug.Log(ConnectTileData.Show());

    }
    public void ConnectLeft()
    {
        Vector2Int targetpos = new Vector2Int(CellCommandPos.x - 1, CellCommandPos.y);
        //右端なら何もしない
        if(targetpos.x < 0){ return; }

        Connect(targetpos);
    }

}
