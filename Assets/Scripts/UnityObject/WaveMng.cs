using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveMng : MonoBehaviour
{
    [SerializeField]
    private EnemyMng EMng;

    void Start()
    {
        // プレイヤーデータのロード
        // モンスターデータのロード

        WaveStart();
    }

    private void WaveStart()
    {
        //EMng.SetEnemies();
    }
}
