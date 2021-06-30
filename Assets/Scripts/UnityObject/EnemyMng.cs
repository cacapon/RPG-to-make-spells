using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMng : MonoBehaviour
{
    [SerializeField]
    private EnemyList EnemyList;

    [SerializeField]
    private UniEnemies Enemies;


    public void SetEnemies()
    {
        List<EnemyData> AppearedEnemy = EnemyChoices(EnemyList.Enemy, Random.Range(1, 4));

        foreach (EnemyData e in AppearedEnemy)
        {
            Enemies.SetEnemy(e);
            Debug.Log(e.name + "があらわれた！");
        }
    }

    private List<EnemyData> EnemyChoices(List<EnemyData> population, int k = 1)
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
