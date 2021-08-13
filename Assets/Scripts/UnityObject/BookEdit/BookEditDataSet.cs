using System.Collections.Generic;
using UnityEngine;
public class BookEditDataSet : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    public Dictionary<string,int> Inventory { get => playerData.Inventory; set => playerData.Inventory = value; }

    public void Initialize(PlayerData pData)
    {
        Inventory = pData.Inventory;
    }

}
