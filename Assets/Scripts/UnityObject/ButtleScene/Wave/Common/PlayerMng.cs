using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMng : MonoBehaviour
{
    public GameMng GMng;
    public PlayerData PData;

    [SerializeField]
    private UniObjShake UIShake;

    [SerializeField]
    private SoundEffect SE;

    [SerializeField]
    private List<AudioClip> SEList;

    private HP HP;

    private MP MP;

    // Start is called before the first frame update
    void Start()
    {
        HP = new HP(PData);
        MP = new MP(PData);
    }

    // Update is called once per frame
    void Update()
    {
        HP.PersistentHP(deltaHP: GMng.WMng.GameSpeed * GMng.PlayerHPDefaultSpeed * Time.deltaTime);
        MP.ChangeMP(deltaMP: GMng.WMng.GameSpeed * PData.MPSpeed * Time.deltaTime);

        if(PData.CurrentHP <= 0f)
        {
            GMng.GameOver();
        }
    }

    public void Attack(Magic magic)
    {
        if (SpendMP(magic.SpendMP)){
            SelectSpell(magic);
            SpellSE();
        }
        else{
            SpellFailedSE();
            Debug.Log("MP タリナイ！");
        }
    }

    private void SelectSpell(Magic magic)
    {
        switch(magic.Type)
        {
            case Magic.MagicType.HEAL:
                Heal(magic.Power);
                break;
            case Magic.MagicType.DAMAGE:
                GMng.EMng.Damage(magic.Power);
                break;
            default:
                break;
        }
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
            SE.PlayOneShot(SEList[1]); //被ダメ大
        }
        else
        {
            SE.PlayOneShot(SEList[0]); //被ダメ少
        }
    }

    private void SpellSE()
    {
        SE.PlayOneShot(SEList[2]); //詠唱
    }

    private void SpellFailedSE()
    {
        SE.PlayOneShot(SEList[3]); //詠唱失敗
    }

    #endregion
}
