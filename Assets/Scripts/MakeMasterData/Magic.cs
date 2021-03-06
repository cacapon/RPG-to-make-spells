[System.Serializable]
public class Magic
{
    public enum eMagicType
    {
        HEAL,
        DAMAGE,
    }

    public enum eMagicTarget
    {
        SELF,
        SINGLE_ENEMY,
        ALL_ENEMY,
    }

    public enum eMagicEffect
    {
        FIRE_1,
        HEAL,
        Ice,
        Thunder,
        None,
    }

    public string name; //全角8文字まで

    public eMagicType Type;

    public eMagicTarget Target;

    public eMagicEffect Effect;

    public int SpendMP;
    public int Power;

    public string EffectToString(eMagicEffect effectName){
        switch(effectName)
        {
            case eMagicEffect.FIRE_1:
                return "Fire_1";
            case eMagicEffect.HEAL:
                return "Heal";
            case eMagicEffect.Ice:
                return "Ice";
            case eMagicEffect.Thunder:
                return "Thunder";
            default:
                return "None";
        }
    }
}

