using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEBUG_HP : HPMng
{
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