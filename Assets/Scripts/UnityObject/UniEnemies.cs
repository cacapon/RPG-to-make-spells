using UnityEngine;
using UnityEngine.UI;

public class UniEnemies : MonoBehaviour
{
    public GameObject EnemyPrefub;
    public void SetEnemy(EnemyData eData)
    {
        GameObject enemy = Instantiate(EnemyPrefub);
        enemy.transform.parent = this.transform;
        enemy.transform.localScale = Vector3.one;
        enemy.AddComponent<Image>();
        enemy.GetComponent<Image>().sprite = eData.Graphic;
    }

}
