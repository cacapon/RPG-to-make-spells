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
    //ŠÔŒo‰ß‚ÅUŒ‚‚·‚é
    //“|‚³‚ê‚½‚ç’Ê’m‚·‚é
    //ƒ_ƒ[ƒW‚ğó‚¯‚éB

    public void AttackIntervalCounter(float time)
    {
        _attackInterval -= time;
    }

    public int Attack()
    {
        if (_attackInterval <= 0)
        {
            //ŠÔ‚ğ–ß‚µ‚ÄUŒ‚—Í‚ğ“n‚·
            _attackInterval = _defaultAttackTime;
            return _attackPoint;
        }
        else
        {
            //UŒ‚‚µ‚È‚¢B
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