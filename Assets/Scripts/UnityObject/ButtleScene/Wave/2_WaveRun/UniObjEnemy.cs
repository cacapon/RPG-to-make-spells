using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniObjEnemy : MonoBehaviour,ITap
{
    private EnemyMng EMng;
    private Enemy Enemy;

    private string myname;
    public string MyName { get => myname; }

    public float AttackInterval { get => Enemy.AttackInterval; }

    public Image Icon;

    public void Init(EnemyData eData, EnemyMng eMng)
    {
        //EnemyMngのAwakeで呼ばれる想定です。
        Enemy = new Enemy(eData);
        myname = Enemy.Name;
        EMng = eMng;
        Icon = this.transform.GetChild(0).GetComponent<Image>();
    }

    private void Update() {
        Enemy.AttackIntervalCounter(EMng.ETimer);

        if(IsAttackIntervalOver())
        {
            EMng.Attack(Attack());
        }
    }

    private bool IsAttackIntervalOver()
    {
        return Enemy.IsAttackIntervalOver();
    }

    public int Attack()
    {
        return Enemy.Attack();
    }

    public void Damage(int point)
    {
        Enemy.Damage(point);
        if (Enemy.IsDead())
        {
            EMng.RemoveEnemy(gameObject);
        }
    }

    public void Tap()
    {
        EMng.SetTarget(gameObject);
    }
}
