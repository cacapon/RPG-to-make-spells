using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniObjEnemy : MonoBehaviour
{
    private Enemy Enemy;

    private string myname;

    public string MyName { get => myname;}

    public void Init(EnemyData eData)
    {
        //EnemyMngのAwakeで呼ばれる想定です。
        Enemy = new Enemy(eData);
        myname = Enemy.Name;
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

}
