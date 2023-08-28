using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum Scenes
    {
        Main,
        Game,
    }

    public enum UIEvent
    {
        Click,
        Drag,
        DragEnd,
        DragStart,
    }
    public enum Sounds
    {
        BGM,
        SFX,
        MaxCount
    }
    public enum LV
    {
        Fire,
        Water,
        Grass, 
        AttackSpeed,
        Money,
        // 5+skill
        Explosion,
        Slow,
        Neutralize,
        

        
        MaxCount
    }
    public enum Properties
    {
        Fire,
        Water,
        Grass,
        None,

        MaxCount
    }
    public enum Skills
    {
        Explosion,
        Slow,
        Neutralize,
        

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


    
}
