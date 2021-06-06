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

    [SerializeField]
    private float MaxHP = 100.0f;

    [SerializeField]
    private float HPChangeSpeed = 10.0f;

    private float CurrentHP;

    private float FutureHP;

    private void Awake()
    {
        CurrentHP   = MaxHP;
        FutureHP = MaxHP; 
    }

    private void Update()
    {
        PersistentHP();
        TextMaxHP.text = MaxHP.ToString("N0");
        TextFutureHP.text = FutureHP.ToString("N0");
        TextCurrentHP.text = CurrentHP.ToString("N0");
    } 

    private void PersistentHP()
    {
        // CurrentHP��FutureHP�܂ŏ��X�ɕω�������B

        // �_���[�W�̏ꍇ
        if (CurrentHP > FutureHP)
        {
            CurrentHP -= HPChangeSpeed * Time.deltaTime;
        }

        // �񕜂̏ꍇ
        if (CurrentHP < FutureHP)
        {
            CurrentHP += HPChangeSpeed * Time.deltaTime;
        }

    }

    public void HPWhenWinning()
    {
        // �������͌����Ă���Ƃ��͍���HP
        // �����Ă���Ƃ���FutureHP�ɂ���B
        if (CurrentHP > FutureHP)
        {
            FutureHP = CurrentHP;
        }

        if (CurrentHP < FutureHP)
        {
            CurrentHP = FutureHP;
        }


    }

    public void ChangeHP(int deltaHP)
    {
        Debug.Log("called ChangeHP func");
        FutureHP += deltaHP;

        if (FutureHP <= 0) 
        {
            FutureHP = 0;
        }
        if (FutureHP >= MaxHP) 
        {
            FutureHP = MaxHP;
        }
    }
}
