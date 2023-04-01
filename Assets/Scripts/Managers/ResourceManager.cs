using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager 
{
    Action MonsterMove = null;
    
    public List<GameObject> _monster_List = new List<GameObject>();
    public List<GameObject> Monster_List { get { return _monster_List; }set { _monster_List = value; } }

    public List<GameObject> InAreaMonster_List = new List<GameObject>();//3*3범위 몬스터

    int idx;


    //List 몬스터;
    //List 투사체;

    public void OnUpdate()
    {
        // Null?? 죽어서 제거했음에도 계속 대행자가 부른다 왜?
        //MonsterMove.Invoke();
    }

    // Start is called before the first frame update
   
}
