using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeleteTower_Controller : MonoBehaviour
{
    
    // Update is called once per frame
    
    static string Effect_tower_wajang_chang = "Effect/tower_wajang_chang";
    Active_Delete_Tower _trigger;
    private void OnMouseDown()
    {
        
        GameManager.Sound.Play(Effect_tower_wajang_chang);
        GameManager.Data.Money += 10;
        _trigger.DeleteAction();
        transform.parent.GetComponent<T_Controller>().SelfDestroy();
    }
    public void GetTrigger(Active_Delete_Tower trigger)
    {
        _trigger = trigger;
    }
    public void DeleteTower()
    {
        try
        {
            Transform par = transform.parent.parent;
            int childnum = par.childCount;
            for(int i = 0; i < childnum; i++)
            {
                if(par.GetChild(i).name[0] == 'T')
                {
                    Destroy(par.GetChild(i).gameObject);
                    break;
                }
            }
        }
        catch (Exception e)
        {
        }

    }

}
