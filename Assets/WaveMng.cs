using System.Collections.Generic;
using UnityEngine;

public class WaveMng : MonoBehaviour
{
    public PlayerData P;
    public EnemyList EnemyList;
    void Start()
    {
        // プレイヤーデータのロード
        // モンスターデータのロード

        WaveStart();
    }

    void WaveStart()
    {
        List<EnemyData> AppearedEnemy = EnemyChoices(EnemyList.Enemy, Random.Range(1, 3));

        foreach (EnemyData e in AppearedEnemy)
        {
            Debug.Log(e.name + "があらわれた！");
        }
    }

    private List<EnemyData> EnemyChoices(List<EnemyData> population, int k=1)
    {
        //指定の敵リストから,kで指定した数だけ敵を選択します。(重複あり)

        List<EnemyData> AppearedEnemy = new List<EnemyData>();
        for (int i = 1; i <= k; i++)
        {
            AppearedEnemy.Add(
                population[Random.Range(0, population.Count)]
                );
        }

        return AppearedEnemy;
    }
}
