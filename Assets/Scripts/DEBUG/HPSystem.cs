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

    public PlayerData P;

    private HP HP;

    private void Awake() {
        HP = new HP(P);
    }

    private void Update()
    {
        TextMaxHP.text     = P.MaxHP.ToString("N1");
        TextFutureHP.text  = P.FutureHP.ToString("N1");
        TextCurrentHP.text = P.CurrentHP.ToString("N1");
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