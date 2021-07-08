using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniObjEnemy : MonoBehaviour,ITap
{
    private EnemyMng EMng;
    private Enemy Enemy;

    [SerializeField] private ShowDamagePoint ShowDamagePoint;
    [SerializeField] private EnemyDeadAnimation DeadAnimation;


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
        StartCoroutine(DamageAnimation(point));
    }

    IEnumerator DamageAnimation(int point)
    {
        ShowDamagePoint.SetDamagePoint(point);
        yield return new WaitForSeconds(0.5f);
        Enemy.Damage(point);
        if (Enemy.IsDead())
        {
            DeadAnimation.PlayAnimation();
            yield return new WaitForSeconds(0.2f);
            EMng.RemoveEnemy(gameObject);
        }
    }

    public void Tap()
    {
        EMng.SetTarget(gameObject);
    }
}
