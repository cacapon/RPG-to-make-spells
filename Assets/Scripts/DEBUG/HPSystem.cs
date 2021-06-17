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

    private HP HP;

    private void Awake()
    {
        HP = new HP(MaxHP);
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