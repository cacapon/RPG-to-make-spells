using UnityEngine;

public class HP
{
    public PlayerData P = null;

    public HP(PlayerData pData = null)
    {
        // PlayerDataの優先度 アタッチ > コンストラクタ
        if (P != null)
        {
            InitHP();
        }
        else if (pData != null)
        {
            P = pData;
            InitHP();
        }
    }

    public void InitHP()
    {
        P.CurrentHP = P.InitHP;
        P.MaxHP = P.InitHP;
        P.FutureHP = P.InitHP;
    }

    public void PersistentHP(float deltaHP)
    {

        if (P.FutureHP > P.CurrentHP)
        {
            P.CurrentHP += deltaHP;
            if (P.FutureHP <= P.CurrentHP)
            {
                P.CurrentHP = P.FutureHP;
            }
        }
        if (P.FutureHP < P.CurrentHP)
        {
            P.CurrentHP -= deltaHP;
            if (P.FutureHP >= P.CurrentHP)
            {
                P.CurrentHP = P.FutureHP;
            }
        }
    }

    public void ChangeHP(int deltaHP)
    {
        P.FutureHP += deltaHP;

        if (P.FutureHP <= 0)
        {
            P.FutureHP = 0;
        }
        if (P.FutureHP >= P.MaxHP)
        {
            P.FutureHP = P.MaxHP;
        }
    }

    public void HPWhenWinning()
    {
        //勝利した際に、Futureとの差分を修正します。

        // ダメージを受けている最中はCurrentに合わせます。
        if (P.CurrentHP > P.FutureHP)
        {
            P.FutureHP = Floor(P.CurrentHP);
        }

        // 回復中の場合はFutureに合わせます。
        if (P.CurrentHP < P.FutureHP)
        {
            P.CurrentHP = Floor(P.FutureHP);
        }
    }

    private float Floor(float hp)
    {
        // 小数点以下の値になるのは望ましくないのでFloorで小数点以下を切り捨てています。
        return Mathf.Floor(hp);
    }
}