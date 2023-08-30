using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeleteTower_Controller : MonoBehaviour
{
    
    // Update is called once per frame
    
    
    private void OnMouseDown()
    {
        GameObject DeleteTower = GameManager.Input.GetClicked2DObject(1 << 9);
        if (DeleteTower != null && DeleteTower == gameObject)
        {
            GameManager.Sound.Play("Effect/tower_wajang_chang");
            GameManager.Data.Money += 10;
            transform.parent.GetComponent<T_Controller>().SelfDestroy();
        }
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
