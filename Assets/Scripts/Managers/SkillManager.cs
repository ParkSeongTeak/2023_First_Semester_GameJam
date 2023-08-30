using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    int explosionDamage = 300;
    float skillRange = 1.5f;//범위내
    List<GameObject> InArea = new List<GameObject>();

    
    // Start is called before the first frame update
    void Start()
    {
        InArea = gameObject.GetComponent<ResourceManager>().InAreaMonster_List;
    }

    //폭발 물약
    public void Explosion()
    {
        //비용이 충분한가?
        if(GameManager.Data.Money >= GameManager.Data.UPGRATECOST[GameManager.Data.LV[2]])
        {
            GameManager.Data.Money -= GameManager.Data.UPGRATECOST[GameManager.Data.LV[2]];

            //범위내 몬스터 공격
            for(int i=0;i<InArea.Count; i++)
            {
                
            }
            
        }
    }
}
