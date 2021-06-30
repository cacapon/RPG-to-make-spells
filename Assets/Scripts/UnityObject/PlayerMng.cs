using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMng : MonoBehaviour
{
    public GameManager GameMng;
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
        HP.PersistentHP(deltaHP: GameMng.GameSpeed * Time.deltaTime);
        MP.ChangeMP(deltaMP: GameMng.GameSpeed * Time.deltaTime);
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
