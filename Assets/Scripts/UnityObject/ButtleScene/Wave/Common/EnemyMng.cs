using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMng : MonoBehaviour
{
    [SerializeField] Dataset dataset;

    [SerializeField] private GameObject EField;
    [SerializeField] private GameObject EPrefub;
    [SerializeField] private GameMng GMng;
    [SerializeField] GameObject PauseMask;

    private GameObject TargetObj = null;

    private UniObjEnemy TargetObjScript = null;


    private float eTimer;
    public float ETimer{get{return eTimer;}}

    private List<EnemyData> ChoiceEnemiesData;

    #region 生成関連のメソッド
    public void SetEnemyfield()
    {

        // EnemyFieldに生成したEnemyObjectをセッティングします。
        if (dataset.CurrentWaveCount == dataset.WaveCount)
        {
            ChoiceEnemiesData = dataset.BossEnemyList;
        }
        else
        {
            ChoiceEnemiesData = EnemyChoices(dataset.RandomEnemyList, Random.Range(1, 4));
        }

        foreach(EnemyData eData in ChoiceEnemiesData)
        {
            GameObject enemy = CreateEnemy(eData);
            enemy.transform.SetParent(EField.transform);
            enemy.transform.localPosition = Vector3.zero;
            enemy.transform.localScale = Vector3.one;
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

    private GameObject CreateEnemy(EnemyData eData)
    {
        //EDataを元にGameObjectを生成する。
        GameObject enemy = Instantiate(EPrefub);
        enemy.GetComponent<Image>().sprite = eData.Graphic;

        //スクリプトの初期化を行う
        UniObjEnemy e = enemy.GetComponent<UniObjEnemy>();
        e.Init(eData, this);

        TouchEvent touch = enemy.GetComponent<TouchEvent>();
        touch.target = enemy;

        return enemy;
    }
    #endregion

    #region 削除関連のメソッド
    public void RemoveEnemy(GameObject target)
    {
        StartCoroutine(DestroyWait(target));
    }
    IEnumerator DestroyWait(GameObject target)
    {
        Destroy(target);

        yield return null;

        if (EField.transform.childCount == 0)
        {
            GMng.Next();
        }
    }
    #endregion

    #region 攻撃関連のメソッド
    public void Damage(Magic magic)
    {
        if (magic.Target == Magic.eMagicTarget.SINGLE_ENEMY)
        {
            //ターゲット未設定なら自動で一番右をターゲットにする。
            if (TargetObj == null)
            {
                SetTarget(GetFirstEnemy());
            }

            TargetObjScript.Damage(magic);
        }
        else if(magic.Target == Magic.eMagicTarget.ALL_ENEMY)
        {
            foreach (Transform enemy in EField.transform)
            {
                Debug.Log(enemy);
                SetTarget(enemy.gameObject);
                TargetObjScript.Damage(magic);
            }
            SetTarget(GetFirstEnemy());
        }
    }

    public void Attack(int point)
    {
        GMng.PMng.Damage(point);
    }

    #endregion

    #region ターゲット設定関連のメソッド

    public void SetTarget(GameObject targetObj)
    {
        if (TargetObj != null){
            //変更前のターゲットアイコンを非表示にしておく。
            TargetObjScript.Icon.enabled = false;
        }

        //ターゲットの設定
        TargetObj = targetObj;
        TargetObjScript = targetObj.GetComponent<UniObjEnemy>();

        //ターゲットアイコンの表示
        TargetObjScript.Icon.enabled = true;
        Debug.Log(TargetObj + "にターゲットを変更しました");
    }

    private GameObject GetFirstEnemy()
    {
        return EField.transform.GetChild(0).gameObject;
    }
    #endregion

    #region Unity Function

    private void Awake() {
        SetEnemyfield();
    }

    private void Update() {
        eTimer = GMng.WMng.GameSpeed * GMng.EnemyAttackDefaultSpeed * Time.deltaTime;
    }

    #endregion

    #region PauseButton関連
    public void PauseButtonEnabled(bool enable)
    {
        PauseMask.SetActive(!enable);
    }
    #endregion
}