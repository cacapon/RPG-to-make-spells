using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList", menuName = "CreateMasterData/EnemyList")]
public class EnemyListData : ScriptableObject
{
    public List<EnemyData> Enemy;
}

