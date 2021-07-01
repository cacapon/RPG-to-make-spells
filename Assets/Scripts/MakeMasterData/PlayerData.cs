using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "CreateMasterData/Player")]
public class PlayerData : ScriptableObject
{
    public string myName;
    public int InitHP;
    public int InitMP;
    public float MPSpeed;

    [NonSerialized]
    public float MaxHP;
    [NonSerialized]
    public float CurrentHP;
    [NonSerialized]
    public float FutureHP;


    [NonSerialized]
    public float MaxMP;
    [NonSerialized]
    public float CurrentMP;
    public Magic[] book;
}