using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySystem : MonoBehaviour
{
    private Enemy Em;

    public HPSystem Hp;

    public int MaxHP = 100;

    public int AttackPoint = 5;

    public float AttackInterval = 3.0f;

    public float CountSpeed = 3.0f;


    [SerializeField]
    private GameObject EnemyObj;

    [SerializeField]
    private Text TextAttackInterval;
    [SerializeField]
    private Slider SlidterHP;



    // Start is called before the first frame update
    void Awake()
    {
        Em = new Enemy(MaxHP,AttackPoint,AttackInterval);
        Em.Name = "Test Enemy"; //HACK: �O���Ɍ��߂�����`�ł����̂��낤���H

        SlidterHP.maxValue = Em.MaxHP;
        SlidterHP.value = Em.CurrentHP;
        SlidterHP.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //�\���֘A�̏���
        TextAttackInterval.text = Em.AttackInterval.ToString("N1");
        SlidterHP.value = Em.CurrentHP;

        // FIXME:�|������ASetActive��False�ɂ��邱�Ƃň�x�����Ă΂�Ă��Ȃ��悤�ɂ��Ă���B����ł������͔����ȏ�
        if (Em.IsDead()){
            Debug.Log(Em.Name + "��|�����I");
            EnemyObj.SetActive(false);
            return;
        }
        else
        {
            //�v�Z�֘A�̏���
            Em.AttackIntervalCounter(Time.deltaTime * CountSpeed); //TODO: MANA��HP�ɂ����ʂ����Q�[�����Ԃ��ǂ����Œ�`����K�v������B
            Hp.Damage(Em.Attack());
        }
    }

    public void YourAttack(int damagePoint)
    {
        Em.Damage(damagePoint);
    }
}

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

    public Enemy(int maxHP, int attackPoint,float AttackInterval)
    {
        _maxHP = maxHP;
        _currentHP = maxHP;
        _attackPoint = attackPoint;

        _attackInterval = AttackInterval;
        _defaultAttackTime = AttackInterval;
    }
    //���Ԍo�߂ōU������
    //�|���ꂽ��ʒm����
    //�_���[�W���󂯂�B

    public void AttackIntervalCounter(float time)
    {
        _attackInterval -= time;
    }

    public int Attack()
    {
        if (_attackInterval <= 0)
        {
            //���Ԃ�߂��čU���͂�n��
            _attackInterval = _defaultAttackTime;
            return _attackPoint;
        }
        else
        {
            //�U�����Ȃ��B
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