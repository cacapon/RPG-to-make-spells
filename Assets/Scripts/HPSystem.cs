using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSystem : MonoBehaviour
{
    [SerializeField]
    private Text TextMaxHP;
    [SerializeField]
    private Text TextCurrentHP;
    [SerializeField]
    private Text TextFutureHP;

    
    public float MaxHP = 100.0f;

    public float HPChangeSpeed = 10.0f;

    private HitPoint HP;

    private void Awake()
    {
        HP = new HitPoint(MaxHP);
    }

    private void Update()
    {
        HP.PersistentHP(Time.deltaTime * HPChangeSpeed);
        TextMaxHP.text     = HP.MaxHP.ToString("N1");
        TextFutureHP.text  = HP.FutureHP.ToString("N1");
        TextCurrentHP.text = HP.CurrentHP.ToString("N1");
    }

    public void Damage(int damagePoint)
    {
        HP.ChangeHP(-damagePoint);
    }

    public void Heal(int healPoint)
    {
        HP.ChangeHP(healPoint);
    }

    public void Won()
    {
        HP.HPWhenWinning();
    }
}

public class HitPoint
{
    private float _currentHP;
    public float CurrentHP {
        get { return _currentHP; }
    }

    private float _futureHP;
    public float FutureHP {
        get { return _futureHP; }
    }

    private float _maxHP;
    public float MaxHP {
        get { return _maxHP; }
    }


    public HitPoint(float maxHP)
    {
        _maxHP = maxHP;
        _futureHP = maxHP;
        _currentHP = maxHP;
    }

    public void PersistentHP(float deltaHP)
    {
        // _currentHPを_futureHPまで徐々に変化させる。

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
        // 勝利時は減っているときは今のHP
        // 増えているときはFutureHPにする。
        // HPが確定したタイミングで小数点以下のHPが決まる可能性があるので、切り捨て処理を入れている。

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
        // HPが小数点以下にならないことを保証したい
        return Mathf.Floor(hp);
    }
}