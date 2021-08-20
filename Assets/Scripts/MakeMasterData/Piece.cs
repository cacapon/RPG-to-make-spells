using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Piece
{
    public string PieceID;
    public int Color;
    public Vector2Int Plug; //TODO:内部ではPlugもSocketも同じ扱いにしたい
    public Vector2Int Socket;//TODO:内部ではPlugもSocketも同じ扱いにしたい
    public Vector2Int ShapeSize;
    public List<Vector2Int> Shape;
}

