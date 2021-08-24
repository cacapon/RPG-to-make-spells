using System;

[Serializable]
public class Magic2 //TODO:後でMagicに改名する
{
    public enum eMagicMain
    {
        FIRE,
        THUNDER,
        ICE,
        NONE,
    }

    public enum eMagicTarget
    {
        ME,
        YOUR,
    }

    public enum eMagicRange
    {
        SINGLE,
        MULTI,
    }

    public string name; //全角8文字まで
    public eMagicMain Main;
    public eMagicTarget Target;
    public eMagicRange Range;
    public bool Reverse;
    public int SpendMP;
    public int Power;
    public bool IsEstablish { get => Main != eMagicMain.NONE; }

    public Magic2()
    {
        Main = eMagicMain.NONE;
        Target = eMagicTarget.YOUR;
        Range = eMagicRange.SINGLE;
        Reverse = false;
    }

    public string Effect()
    {
        switch (Main)
        {
            case eMagicMain.FIRE:
                if (Target == eMagicTarget.ME) { return "FireHeal"; }
                else { return "fire"; }
            case eMagicMain.ICE:
                if (Target == eMagicTarget.ME) { return "IceHeal"; }
                else { return "Ice"; }
            case eMagicMain.THUNDER:
                if (Target == eMagicTarget.ME) { return "ThunderHeal"; }
                else { return "Thunder"; }
            default:
                return "None";
        }
    }

    public void SetPowerAndMP()
    {
        //          DM  MP
        //ICE       5   5
        //FIRE      15  10
        //THUNDER   35  20
        //Multi     X1  X2
        if(IsEstablish)
        {
            switch(Main)
            {
                case eMagicMain.ICE:
                    Power = 5;
                    SpendMP = 5;
                    break;
                case eMagicMain.FIRE:
                    Power = 15;
                    SpendMP = 10;
                    break;
                case eMagicMain.THUNDER:
                    Power = 35;
                    SpendMP = 20;
                    break;
                default:
                    throw new Exception("");
            }

            if(Range == eMagicRange.MULTI)
            {
                SpendMP = SpendMP*2;
            }
        }
    }
    public void ConvertMagic(string RuneData)
    {
        //RuneDataから魔法の要素を選択する
        switch (RuneData)
        {
            case "ME":
                Target = eMagicTarget.ME;
                break;
            case "YOUR":
                Target = eMagicTarget.YOUR;
                break;
            case "SINGLE":
                Range = eMagicRange.SINGLE;
                break;
            case "MULTI":
                Range = eMagicRange.MULTI;
                break;
            case "REVERCE":
                Reverse = !Reverse;
                break;
            case "FIRE":
                Main = eMagicMain.FIRE;
                break;
            case "ICE":
                Main = eMagicMain.ICE;
                break;
            case "THUNDER":
                Main = eMagicMain.THUNDER;
                break;
            default:
                throw new Exception($"想定外のデータ:{RuneData}");
        }
    }
}

