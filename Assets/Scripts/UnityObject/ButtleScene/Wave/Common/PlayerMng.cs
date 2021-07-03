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
        HP.PersistentHP(deltaHP: GMng.GameSpeed * GMng.PlayerHPDefaultSpeed * Time.deltaTime);
        MP.ChangeMP(deltaMP: GMng.GameSpeed * PData.MPSpeed * Time.deltaTime);

        if(PData.CurrentHP <= 0f)
        {
            GMng.GameOver();
        }
    }

    public void Attack(Magic magic)
    {
        if (SpendMP(magic.SpendMP)){
            GMng.EMng.Damage(magic.Power);
        }
        else{
            Debug.Log("MP タリナイ！");
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
    #endregion
}
