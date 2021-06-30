using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPMng : MonoBehaviour
{
    public GameManager GameMng;

    public PlayerData PlayerData;

    protected MP MP;

    void Awake()
    {
        MP = new MP(PlayerData);
    }

    // Update is called once per frame
    void Update()
    {
        MP.ChangeMP(deltaMP: GameMng.GameSpeed * Time.deltaTime);
    }
}
