using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMng : MonoBehaviour
{
    public GameMng GMng;

    [SerializeField] Dataset dataset;

    [SerializeField] private UniObjShake UIShake;

    [SerializeField] private SoundEffect SE;

    private HP HP;

    private MP MP;

    private bool isArive;

    // Start is called before the first frame update
    void Start()
    {
        HP = new HP(dataset.PlayerData);
        MP = new MP(dataset.PlayerData);
        isArive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isArive)
        {
            HP.PersistentHP(deltaHP: GMng.WMng.GameSpeed * GMng.PlayerHPDefaultSpeed * Time.deltaTime);
            MP.ChangeMP(deltaMP: GMng.WMng.GameSpeed * dataset.MPSpeed * Time.deltaTime);

            if (dataset.CurrentHP <= 0f)
            {
                isArive = false;
                GMng.GameOver();
            }
        }
    }

    public void Attack(Magic magic)
    {
        if (SpendMP(magic.SpendMP))
        {
            SelectSpell(magic);
            SpellSE();
        }
        else
        {
            SpellFailedSE();
            Debug.Log("MP タリナイ！");
        }
    }

    private void SelectSpell(Magic magic)
    {
        StartCoroutine(SpellAnimation(magic));
    }

    IEnumerator SpellAnimation(Magic magic)
    {
        switch (magic.Type)
        {
            case Magic.eMagicType.HEAL:
                Heal(magic.Power);
                break;
            case Magic.eMagicType.DAMAGE:
                GMng.EMng.Damage(magic);
                break;
            default:
                break;
        }
        yield return null;
    }

    public void WaveWinning()
    {
        HP.HPWhenWinning();
    }

    public void Damage(int point)
    {
        HP.ChangeHP(-point);

        Shake();
        DamageSE();
    }
    public void Heal(int point)
    {
        HP.ChangeHP(point);
    }

    public bool SpendMP(int point)
    {
        return MP.SpendMP(point);
    }

    #region エフェクト関連
    private void Shake()
    {
        if (HP.P.FutureHP <= 0)
        {
            UIShake.LargeShake();
        }
        else
        {
            UIShake.SmallShake();
        }
    }

    private void DamageSE()
    {
        if (HP.P.FutureHP <= 0)
        {
            SE.PlayOneShot(SoundEffect.eSEName.DAMAGE_LARGE); //被ダメ大
        }
        else
        {
            SE.PlayOneShot(SoundEffect.eSEName.DAMAGE_SMALL); //被ダメ少
        }
    }

    private void SpellSE()
    {
        SE.PlayOneShot(SoundEffect.eSEName.SPELL); //詠唱
    }

    private void SpellFailedSE()
    {
        SE.PlayOneShot(SoundEffect.eSEName.SPELL_FAILED); //詠唱失敗
    }

    #endregion
}
