using System;
using System.Collections.Generic;

public class Cell
{
    Guid cellid;

    CellType cellType;

    public Cell()
    {
        cellid = Guid.Empty;
        cellType = CellType.None;
    }

    public void Set(Guid id, CellType celltype)
    {
        cellid = id;
        cellType = celltype;
    }

    public CellType Show()
    {
        return cellType;
    }

    public bool isEmpty {get => cellid == Guid.Empty;}
    public Guid Cellid { get => cellid;}
    public CellType CellType { get => cellType;}
}