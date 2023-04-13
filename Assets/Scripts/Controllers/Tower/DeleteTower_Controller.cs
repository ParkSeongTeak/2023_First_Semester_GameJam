using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTower_Controller : MonoBehaviour
{
    
    // Update is called once per frame
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject DeleteTower = GameManager.Input.GetClicked2DObject(1 << 9);
            if (DeleteTower != null && DeleteTower == gameObject)
            {
                GameManager.Sound.Play("Effect/tower_wajang_chang");
                GameManager.Instance.Money += 10;
                transform.parent.GetComponent<T_Controller>().SelfDestroy();
            }
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
            // 거의 불가능하지만 계산 중 skill이 사라지면서 버그가 날 수 있다. 그런 극한의 상황을 처리해줄 바에 키 씹힘으로 하고  넘기자
        }

    }
}
