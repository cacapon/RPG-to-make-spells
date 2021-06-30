using Random = UnityEngine.Random;

public class Enemy
{
    public Enemy(EnemyData eData)
    {
        Name = eData.name;
        _maxHP = eData.InitHP;
        _currentHP = eData.InitHP;
        _attackPoint = eData.Attack;

        _attackInterval = eData.Duration;
        _defaultAttackTime = eData.Duration + Random.Range(-1f,1f);
    }

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

    public void AttackIntervalCounter(float time)
    {
        _attackInterval -= time;
    }

    public int Attack()
    {
        if (_attackInterval <= 0)
        {
            _attackInterval = _defaultAttackTime;
            return _attackPoint;
        }
        else
        {
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