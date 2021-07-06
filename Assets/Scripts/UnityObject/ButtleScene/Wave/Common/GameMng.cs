using UnityEngine;

public class GameMng : MonoBehaviour {
    public float PlayerHPDefaultSpeed = 10.0f;
    public float EnemyAttackDefaultSpeed = 1.0f;
    public PlayerMng PMng;
    public EnemyMng EMng;
    public WaveMng WMng;

    [SerializeField]
    private BGM BGM;
    private bool gameClearFlg;

    private void Awake() {
        gameClearFlg = false;
        BGM.SetBGM(0); // Buttle1
    }

    public void Next()
    {
        PMng.WaveWinning();
        WMng.BSData.CurrentWaveCount++;

        if(WMng.BSData.CurrentWaveCount < WMng.BSData.MaxWaveCount)
        {
            //通常戦闘
            EMng.SetEnemyfield();
            WMng.NextWave();
        }
        else if(WMng.BSData.CurrentWaveCount == WMng.BSData.MaxWaveCount)
        {
            BGM.StopBGM();
            //ボス戦闘
            EMng.SetEnemyfield();
            WMng.NextWave();
            BGM.SetBGM(1); // BOSS戦音楽
        }
        else{
            //クリア
            BGM.StopBGM();
            GameClear();
        }

    }

    public void GameOver()
    {
        if(!gameClearFlg)
        {
            Debug.Log("ゲームオーバー");
            WMng.GameOver();
        }
    }

    public void GameClear()
    {
        Debug.Log("ゲームクリア");
        WMng.GameClear();
    }
}
