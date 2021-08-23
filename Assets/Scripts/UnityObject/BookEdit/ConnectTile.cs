using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConnectTile : MonoBehaviour
{
    [SerializeField] private List<TileBase> tiles;
    [SerializeField] private ConnectTileData connectTileData;
    private Tilemap Mytilemap;

    private static int StageSize = 7;

    private void Awake()
    {
        Mytilemap = GetComponent<Tilemap>();
    }

    private void Start() {
        SetConnectTile();
    }

    public void SetConnectTile()
    {
        for (int v = 0; v < StageSize; v++)
        {
            for (int h = 0; h < StageSize; h++)
            {
                Mytilemap.SetTile(new Vector3Int(v, -h, 0), tiles[(int)connectTileData.TileData[h][v]]);
            }
        }
    }
}
