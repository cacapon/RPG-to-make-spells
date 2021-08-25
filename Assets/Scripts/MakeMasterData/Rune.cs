using System;

[Serializable]
public class Rune
{
    public string RuneID;
    public string PieceID;
    public string Type;

    /// <summary>
    /// Runeの説明を返します
    /// </summary>
    /// <param name="Type"></param>
    /// <returns></returns>
    public string Description(string Type)
    {
        switch (Type)
        {
            case "ME":
                return "ＭＥ\nターゲットを自分にします";
            case "MULTI":
                return "ＭＵＬＴＩ　消費ＭＰ２倍\n範囲を全体にします";
            case "REVERCE":
                return "ＲＥＶＥＲＳＥ\n効果を反転させます";
            case "FIRE":
                return "ＦＩＲＥ\n標準的な攻撃魔法";
            case "ICE":
                return "ＩＣＥ\n消費ＭＰが控えめな攻撃魔法";
            case "THUNDER":
                return "ＴＨＵＮＤＥＲ\nダメージ力が高い攻撃魔法";
            default:
                throw new Exception($"想定外のデータ:{Type}");
        }
    }
}