using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveMng : MonoBehaviour
{
    [SerializeField]
    private EnemyMng EMng;

    [SerializeField]
    private List<GameObject> Flows;

    void Start()
    {
        // プレイヤーデータのロード
        // モンスターデータのロード

        WaveStart();
    }

    private void WaveStart()
    {
        Flows[0].SetActive(true);
        Flows[1].SetActive(false);
    }

    public void GameOver()
    {
        Flows[0].SetActive(false);
        Flows[1].SetActive(true);
    }

}
