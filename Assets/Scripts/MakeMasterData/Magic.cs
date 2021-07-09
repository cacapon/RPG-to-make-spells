[System.Serializable]
public class Magic
{
    public enum eMagicType
    {
        HEAL,
        DAMAGE,
    }

    public enum eMagicEffect
    {
        FIRE_1,
        None,
    }

    public string name; //全角8文字まで

    public eMagicType Type;

    public eMagicEffect Effect;

    public int SpendMP;
    public int Power;

    public string EffectToString(eMagicEffect effectName){
        switch(effectName)
        {
            case eMagicEffect.FIRE_1:
                return "Fire_1";
            default:
                return "None";
        }
    }
}

