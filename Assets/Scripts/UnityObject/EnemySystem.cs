using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySystem : MonoBehaviour
{
    private Enemy Em;

    public HPSystem Hp;

    public int MaxHP = 100;

    public int AttackPoint = 5;

    public float AttackInterval = 3.0f;

    public float CountSpeed = 3.0f;


    [SerializeField]
    private GameObject EnemyObj;

    [SerializeField]
    private Text TextAttackInterval;
    [SerializeField]
    private Slider SlidterHP;



    // Start is called before the first frame update
    void Awake()
    {
        Em = new Enemy(MaxHP,AttackPoint,AttackInterval);
        Em.Name = "Test Enemy"; //HACK: 外部に決めさせる形でいいのだろうか？

        SlidterHP.maxValue = Em.MaxHP;
        SlidterHP.value = Em.CurrentHP;
        SlidterHP.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //表示関連の処理
        TextAttackInterval.text = Em.AttackInterval.ToString("N1");
        SlidterHP.value = Em.CurrentHP;

        // FIXME:倒した後、SetActiveをFalseにすることで一度しか呼ばれていないようにしている。これでいいかは微妙な所
        if (Em.IsDead()){
            Debug.Log(Em.Name + "を倒した！");
            EnemyObj.SetActive(false);
            return;
        }
        else
        {
            //計算関連の処理
            Em.AttackIntervalCounter(Time.deltaTime * CountSpeed); //TODO: MANAやHPにも共通したゲーム時間をどこかで定義する必要がある。
            Hp.Damage(Em.Attack());
        }
    }

    public void YourAttack(int damagePoint)
    {
        Em.Damage(damagePoint);
    }
}