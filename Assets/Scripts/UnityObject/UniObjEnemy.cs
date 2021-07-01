using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniObjEnemy : MonoBehaviour,ITap
{
    private EnemyMng EMng;
    private Enemy Enemy;

    private string myname;
    public string MyName { get => myname;}

    public Image Icon;

    public void Init(EnemyData eData, EnemyMng eMng)
    {
        //EnemyMngのAwakeで呼ばれる想定です。
        Enemy = new Enemy(eData);
        myname = Enemy.Name;
        EMng = eMng;
        Icon = this.transform.GetChild(0).GetComponent<Image>();
    }

    public void Timer(float time)
    {
        Enemy.AttackIntervalCounter(time);
    }

    public bool IsAttackIntervalOver()
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
    }

    public void Tap()
    {
        EMng.SetTarget(this);
    }
}
