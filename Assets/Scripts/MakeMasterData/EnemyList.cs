using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList", menuName = "CreateMasterData/EnemyList")]
public class EnemyList : ScriptableObject
{
    public List<EnemyData> Enemy;
}

