using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMng : MonoBehaviour
{

    [SerializeField]
    private EnemyListData EListData;

    public GameManager GMng;

    [SerializeField]
    private GameObject EPrefub;

    [SerializeField]
    private GameObject EField;

    private List<GameObject> Enemies;


    private List<EnemyData> AppearedEnemies;

    private void Awake() {
        // Enemyの生成
        Enemies = new List<GameObject>();
        SetEnemies();
    }

    private void Update() {
        // Enemyの時間管理

        foreach (GameObject Enemy in Enemies)
        {
            UniObjEnemy E = Enemy.GetComponent<UniObjEnemy>();
            E.Timer(Time.deltaTime);

            if(E.IsAttackIntervalOver())
            {
                Debug.Log(E.MyName + "の攻撃！");
                Attack(E.Attack());
            }
        }
    }

    public void SetEnemies()
    {
        AppearedEnemies = EnemyChoices(EListData.Enemy, Random.Range(1, 4));

        foreach (EnemyData e in AppearedEnemies)
        {
            Enemies.Add(SetEnemy(e));
            Debug.Log(e.name + "があらわれた！");
        }
    }

    private List<EnemyData> EnemyChoices(List<EnemyData> population, int k = 1)
    {
        //指定の敵リストから,kで指定した数だけ敵を選択します。(重複あり)
        List<EnemyData> choices = new List<EnemyData>();
        for (int i = 1; i <= k; i++)
        {
            choices.Add(
                population[Random.Range(0, population.Count)]
                );
        }
        return choices;
    }

    public GameObject SetEnemy(EnemyData eData)
    {
        GameObject enemy = Instantiate(EPrefub);
        enemy.transform.SetParent(EField.transform);
        enemy.transform.localPosition = Vector3.zero;
        enemy.transform.localScale = Vector3.one;
        enemy.GetComponent<Image>().sprite = eData.Graphic;

        //スクリプトの初期化
        UniObjEnemy e = enemy.GetComponent<UniObjEnemy>();
        e.Init(eData);

        return enemy;
    }

    public void Damage(int point)
    {

    }

    public void Attack(int point)
    {
        GMng.PMng.Damage(point);
    }
}
