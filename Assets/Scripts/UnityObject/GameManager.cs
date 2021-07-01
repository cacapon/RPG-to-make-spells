using UnityEngine;

public class GameManager : MonoBehaviour {
    public float GameSpeed = 1.0f;
    public float PlayerHPDefaultSpeed = 10.0f;
    public float EnemyAttackDefaultSpeed = 1.0f;
    public PlayerMng PMng;
    public EnemyMng EMng;
    public WaveMng WMng;

    public void GameOver()
    {
        GameSpeed = 0.0f;
        Debug.Log("ゲームオーバー");
        WMng.GameOver();
    }
}
