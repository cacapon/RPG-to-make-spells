public class Enemy
{
    public string Name { get; set; }
    private int _maxHP;
    public int MaxHP
    {
        get { return _maxHP; }
    }

    private int _currentHP;
    public int CurrentHP
    {
        get { return _currentHP; }
    }

    public int _attackPoint;
    public int AttackPoint
    {
        get { return _attackPoint; }
    }


    private float _attackInterval;
    public float AttackInterval
    {
        get { return _attackInterval; }
    }


    private float _defaultAttackTime;

    public Enemy(int maxHP, int attackPoint, float AttackInterval)
    {
        _maxHP = maxHP;
        _currentHP = maxHP;
        _attackPoint = attackPoint;

        _attackInterval = AttackInterval;
        _defaultAttackTime = AttackInterval;
    }
    //時間経過で攻撃する
    //倒されたら通知する
    //ダメージを受ける。

    public void AttackIntervalCounter(float time)
    {
        _attackInterval -= time;
    }

    public int Attack()
    {
        if (_attackInterval <= 0)
        {
            //時間を戻して攻撃力を渡す
            _attackInterval = _defaultAttackTime;
            return _attackPoint;
        }
        else
        {
            //攻撃しない。
            return 0;
        }
    }

    public void Damage(int damagePoint)
    {
        _currentHP -= damagePoint;
    }

    public bool IsDead()
    {
        return _currentHP <= 0;
    }
}