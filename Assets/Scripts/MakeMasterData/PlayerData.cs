using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "CreateMasterData/Player")]
public class PlayerData : ScriptableObject
{
    public int InitHP;
    public int InitMP;
    public List<Magic> Book;
}
