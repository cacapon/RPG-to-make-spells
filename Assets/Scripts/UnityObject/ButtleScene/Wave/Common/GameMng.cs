using UnityEngine;

public class GameMng : MonoBehaviour {
    public float PlayerHPDefaultSpeed = 10.0f;
    public float EnemyAttackDefaultSpeed = 1.0f;

    [SerializeField] private Dataset dataset;
    public PlayerMng PMng;
    public EnemyMng EMng;
    public WaveMng WMng;
    private bool gameClearFlg;

    public BGM BGM;
    public Jingle Jingle;

    private void Awake() {
        gameClearFlg = false;
        BGM.Play(BGM.eBGMName.BUTTLE1);

    }

    public void Next()
    {
        PMng.WaveWinning();
        WMng.IncrementWaveCount();

        if(WMng.IsButtle())
        {
            //戦闘
            EMng.SetEnemyfield();
            WMng.NextWave();
        }
        else{
            //クリア
            GameClear();
        }
    }

    public void GameOver()
    {
        if(!gameClearFlg)
        {
            Debug.Log("ゲームオーバー");
            WMng.GameOver();
            BGM.Stop();
            Jingle.Play(Jingle.eJingleName.GAMEOVER);
        }
    }

    public void GameClear()
    {
        Debug.Log("ゲームクリア");
        WMng.GameClear();
        BGM.Stop();
        Jingle.Play(Jingle.eJingleName.GAMECLEAR);
    }
}
