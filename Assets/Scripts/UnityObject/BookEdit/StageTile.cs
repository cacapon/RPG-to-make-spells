using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageTile : MonoBehaviour
{
    [SerializeField] private List<TileBase> tiles;
    [SerializeField] private StageData stagedata;
    private Tilemap Mytilemap;

    private static int StageSize = 7;

    private void Awake()
    {
        Mytilemap = GetComponent<Tilemap>();
    }

    private void Start() {
        SetStageTile();
    }

    public void SetStageTile()
    {
        for (int v = 0; v < StageSize; v++)
        {
            for (int h = 0; h < StageSize; h++)
            {
                Mytilemap.SetTile(new Vector3Int(v, -h, 0), tiles[(int)stagedata.Stage[h][v].Show()]);
            }
        }
    }
}
