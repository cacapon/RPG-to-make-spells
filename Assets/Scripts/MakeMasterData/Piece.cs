using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Piece
{
    public string PieceID;
    public int Color;
    public Vector2Int Plug;
    public Vector2Int Socket;
    public Vector2Int ShapeSize;
    public List<Vector2Int> Shape;
}

