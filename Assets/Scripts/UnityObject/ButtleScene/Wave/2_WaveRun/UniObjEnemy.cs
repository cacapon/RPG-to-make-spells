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
    [SerializeField] private SpellAnimation SpellAnimation;

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

    public void Damage(Magic magic)
    {
        StartCoroutine(DamageAnimation(magic));
    }

    IEnumerator DamageAnimation(Magic magic)
    {
        //呪文のエフェクト
        SpellAnimation.PlaySoloAttackAnimation(magic);
        yield return new WaitForAnimation(SpellAnimation.SpellAnimator,0);

        ShowDamagePoint.SetDamagePoint(magic.Power);
        yield return new WaitForAnimation(ShowDamagePoint.Animator,0);

        Enemy.Damage(magic.Power);
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
