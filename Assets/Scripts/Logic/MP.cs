using UnityEngine;

public class MP
{
    private float _currentMP;
    public float CurrentMP
    {
        get { return _currentMP; }
    }

    private float _maxMP;
    public float MaxMP
    {
        get { return _maxMP; }
    }


    public MP(float maxMP)
    {
        _maxMP = maxMP;
        _currentMP = maxMP;
    }


    public void ChangeMP(float deltaMP)
    {
        if (deltaMP < 0 && Mathf.Abs(deltaMP) > _currentMP)
        {
            // ����̍ۂɌ��݂�MP�����Ȃ���Ό��炳�Ȃ��B
            return;
        }

        _currentMP += deltaMP;


        if (_currentMP >= _maxMP)
        {
            _currentMP = _maxMP;
        }
    }
}
