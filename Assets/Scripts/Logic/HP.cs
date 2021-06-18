using UnityEngine;

public class HP
{
    private float _currentHP;
    public float CurrentHP
    {
        get { return _currentHP; }
    }

    private float _futureHP;
    public float FutureHP
    {
        get { return _futureHP; }
    }

    private float _maxHP;
    public float MaxHP
    {
        get { return _maxHP; }
    }


    public HP(float maxHP)
    {
        _maxHP = maxHP;
        _futureHP = maxHP;
        _currentHP = maxHP;
    }

    public void PersistentHP(float deltaHP)
    {
        // _currentHP��_futureHP�܂ŏ��X�ɕω�������B

        if (_futureHP > _currentHP)
        {
            _currentHP += deltaHP;
            if (_futureHP <= _currentHP)
            {
                _currentHP = _futureHP;
            }
        }
        if (_futureHP < _currentHP)
        {
            _currentHP -= deltaHP;
            if (_futureHP >= _currentHP)
            {
                _currentHP = _futureHP;
            }
        }
    }

    public void ChangeHP(int deltaHP)
    {
        _futureHP += deltaHP;

        if (_futureHP <= 0)
        {
            _futureHP = 0;
        }
        if (_futureHP >= _maxHP)
        {
            _futureHP = _maxHP;
        }
    }

    public void HPWhenWinning()
    {
        // �������͌����Ă���Ƃ��͍���HP
        // �����Ă���Ƃ���FutureHP�ɂ���B
        // HP���m�肵���^�C�~���O�ŏ����_�ȉ���HP�����܂�\��������̂ŁA�؂�̂ď��������Ă���B

        if (_currentHP > _futureHP)
        {
            _futureHP = Floor(_currentHP);
        }

        if (_currentHP < _futureHP)
        {
            _currentHP = Floor(_futureHP);
        }
    }

    private float Floor(float hp)
    {
        // HP�������_�ȉ��ɂȂ�Ȃ����Ƃ�ۏ؂�����
        return Mathf.Floor(hp);
    }
}