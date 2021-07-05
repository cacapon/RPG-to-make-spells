[System.Serializable]
public class Magic
{
    public enum MagicType
    {
        HEAL,
        DAMAGE,
    }
    public string name; //全角8文字まで

    public MagicType Type;

    public int SpendMP;
    public int Power;
}