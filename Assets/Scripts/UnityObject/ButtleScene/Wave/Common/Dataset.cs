using System.Collections.Generic;
using UnityEngine;
public class Dataset : MonoBehaviour
{
    [SerializeField] private ButtleSceneData buttleSceneData;
    [SerializeField] private PlayerData playerData;

    #region ButtleSceneData
    public float CurrentWaveCount { get => buttleSceneData.CurrentWaveCount; set => buttleSceneData.CurrentWaveCount = value; }
    //public float MaxWaveCount { get => buttleSceneData.MaxWaveCount; }
    public int WaveCount { get => buttleSceneData.WaveCount; }
    public List<EnemyData> BossEnemyList { get => buttleSceneData.BossEnemyList; }
    public List<EnemyData> RandomEnemyList { get => buttleSceneData.RandomEnemyList; }
    #endregion

    #region PlayerData
    public string MyName { get => PlayerData.myName; }
    public int InitHP { get => PlayerData.InitHP; }
    public int InitMP { get => PlayerData.InitMP; }
    public float MPSpeed { get => PlayerData.MPSpeed; }
    public float MaxHP { get => PlayerData.MaxHP; }
    public float CurrentHP { get => PlayerData.CurrentHP; set => PlayerData.CurrentHP = value; }
    public float FutureHP { get => PlayerData.FutureHP; set => PlayerData.FutureHP = value; }
    public float MaxMP { get => PlayerData.MaxMP; }
    public float CurrentMP { get => PlayerData.CurrentMP; set => PlayerData.CurrentMP = value; }
    public List<Magic> Book { get => PlayerData.Book; }
    public PlayerData PlayerData { get => playerData;}
    #endregion

    public void Initialize(PlayerData pData, ButtleSceneData bsData)
    {
        playerData = pData;
        buttleSceneData = bsData;
    }

}
