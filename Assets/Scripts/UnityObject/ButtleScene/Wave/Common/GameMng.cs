using UnityEngine;

public class GameMng : MonoBehaviour {
    public float PlayerHPDefaultSpeed = 10.0f;
    public float EnemyAttackDefaultSpeed = 1.0f;
    public PlayerMng PMng;
    public EnemyMng EMng;
    public WaveMng WMng;

    public void WaveNext()
    {
        EMng.SetEnemyfield();
        WMng.NextWave();
    }

    public void GameOver()
    {
        Debug.Log("ゲームオーバー");
        //WMng.GameOver();
    }

    public void GameClear()
    {
        Debug.Log("ゲームクリア");
        //WMng.GameClear();
    }
}
