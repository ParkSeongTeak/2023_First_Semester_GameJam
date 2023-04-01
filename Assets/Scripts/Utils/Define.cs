using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    
    public enum Sound
    {
        BGM,
        Effect,
        MaxCount
    }
    public enum LV
    {
        Fire,
        Water,
        Grass, 
        ShootSpeed,
        MoneyGet,
        // 5+skill
        Explosion,
        Nullity,
        Sticky,

        
        MaxCount
    }
    public enum Property
    {
        Fire,
        Water,
        Grass,
        None,

        MaxCount
    }
    public enum Skill
    {
        Explosion,
        Nullity,
        Sticky,

        MaxCount

    }
    public enum ButtonsEnum 
    {
        BackGroundUI,
        FireT,
        FireTLV,
        FireT_Plus,
        WaterT,
        WaterTLV,
        WaterT_Plus,
        GrassT,
        GrassTLV,
        GrassT_Plus,
        UpgradeMoney,
        UpgradeMoneyLV,
        UpgradeMoney_Plus,
        SkillExplosion,
        SkillExplosionLV,
        SkillExplosion_Plus,

        SkillSlow,
        SkillSlowLV,
        SkillSlow_Plus,

        SkillNone,
        SkillNoneLV,
        SkillNone_Plus,
        
        AttackSpead,
        AttackSpeadLV,
        AttackSpead_Plus,
        ShootSpeed,
        MoneyGet,
        MaxCount

    }

    public enum TextEnum
    {
        MoneyPoint,     //
        MaxPoint,       //
        NowPoint,       //
        WavePoint,      //
        NowScoreEnd,    //
        BestScoreEnd,   //
        WaveEnd,        //
        
        MaxCount
    }


    public enum Else
    {
        BackGroundUI,
        MainPage,
        GameOver,
        MaxCount


    }
    
}
