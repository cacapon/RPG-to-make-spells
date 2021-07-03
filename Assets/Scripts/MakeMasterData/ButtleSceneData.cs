using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtleSceneData", menuName = "CreateMasterData/ButtleSceneData")]
public class ButtleSceneData : ScriptableObject
{
    public int WaveCount;

    [NonSerialized]
    public float MaxWaveCount;
    [NonSerialized]
    public float CurrentWaveCount;

    public List<EnemyData> RandomEnemyList;

    public List<EnemyData> BossEnemyList;

}

