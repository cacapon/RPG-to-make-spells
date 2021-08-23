using System;
using System.Collections.Generic;

public class Cell
{
    Guid cellid;

    string runeId;

    CellType cellType;

    bool isConnectCell;

    public Cell()
    {
        cellid = Guid.Empty;
        cellType = CellType.None;
        runeId = "";
    }

    public void Set(Guid instantid, string runeid, CellType celltype)
    {
        cellid = instantid;
        runeId = runeid;
        cellType = celltype;
        isConnectCell = false;
    }

    public void Set(Guid instantid, string runeid, CellType celltype, bool isconnectcell)
    {
        cellid = instantid;
        runeId = runeid;
        cellType = celltype;
        this.isConnectCell = isconnectcell;
    }



    public CellType Show()
    {
        return cellType;
    }

    public bool isEmpty {get => cellid == Guid.Empty;}
    public Guid Cellid { get => cellid;}
    public CellType CellType { get => cellType;}
    public string RuneId { get => runeId;}
    public bool IsConnectCell { get => isConnectCell;}
}