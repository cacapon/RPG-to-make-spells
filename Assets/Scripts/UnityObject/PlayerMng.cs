using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMng : MonoBehaviour
{
    public GameManager GMng;
    public PlayerData PData;

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
        GMng.EMng.Damage(magic.Power);
    }

    public void Damage(int point)
    {
        HP.ChangeHP(-point);
    }

    public void Heal(int point)
    {
        HP.ChangeHP(point);
    }

    public void SpendMP(int point)
    {
        MP.ChangeMP(-point);
    }

}
