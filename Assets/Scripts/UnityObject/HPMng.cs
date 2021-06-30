using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPMng : MonoBehaviour
{
    public GameManager GameMng;

    public PlayerData PlayerData;

    protected HP HP;

    void Awake()
    {
        HP = new HP(PlayerData);
    }

    // Update is called once per frame
    void Update()
    {
        HP.PersistentHP(deltaHP: GameMng.GameSpeed * Time.deltaTime);
    }
}