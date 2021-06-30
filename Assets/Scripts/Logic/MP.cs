using UnityEngine;

public class MP
{
    public PlayerData P = null;

    public MP(PlayerData pData = null)
    {
        // PlayerDataの優先度 アタッチ > コンストラクタ
        if (P != null)
        {
            InitMP();
        }
        else if (pData != null)
        {
            P = pData;
            InitMP();
        }
    }
    private void InitMP()
    {
        P.MaxMP = P.InitMP;
        P.CurrentMP = P.InitMP;
    }
    public void ChangeMP(float deltaMP)
    {
        if (deltaMP < 0 && Mathf.Abs(deltaMP) > P.CurrentMP)
        {
            // 消費量が現在値より少ない場合は何もしない
            return;
        }

        P.CurrentMP += deltaMP;


        if (P.CurrentMP >= P.MaxMP)
        {
            P.CurrentMP = P.MaxMP;
        }
    }
}
