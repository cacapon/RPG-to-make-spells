using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPSystem : MonoBehaviour
{
    [SerializeField]
    private Text TextMaxMP;
    [SerializeField]
    private Text TextCurrentMP;


    public float MaxMP = 100.0f;

    public float MPChangeSpeed = 5.0f;

    private ManaPoint MP;

    private void Awake()
    {
        MP = new ManaPoint(MaxMP);
    }

    private void Update()
    {
        MP.ChangeMP(Time.deltaTime * MPChangeSpeed);
        TextMaxMP.text = MP.MaxMP.ToString("N1");
        TextCurrentMP.text = MP.CurrentMP.ToString("N1");
    }

    public void SpendMP(int damagePoint)
    {
        MP.ChangeMP(-damagePoint);
    }
}

public class ManaPoint
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


    public ManaPoint(float maxMP)
    {
        _maxMP = maxMP;
        _currentMP = maxMP;
    }


    public void ChangeMP(float deltaMP)
    {
        if (deltaMP < 0 && Mathf.Abs(deltaMP) > _currentMP)
        {
            // Á”ï‚ÌÛ‚ÉŒ»Ý‚ÌMP‚ª­‚È‚¯‚ê‚ÎŒ¸‚ç‚³‚È‚¢B
            return;
        }

        _currentMP += deltaMP;


        if (_currentMP >= _maxMP)
        {
            _currentMP = _maxMP;
        }
    }
}
