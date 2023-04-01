using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    [SerializeField]
    GameObject Obj;
    public void SetActiveTrue()
    {
        if (Obj != null)
            Obj.SetActive(true);
    }
    public void SetActiveFalse()
    {
        if (Obj != null)
            Obj.SetActive(false);
    }

    public void MoneySetActiveTrue()
    {
        if (Obj != null && GameManager.Instance.LV[(int)Define.LV.MoneyGet] < 4)
            Obj.SetActive(true);
    }
    public void SpeedSetActiveTrue()
    {
        if (Obj != null && GameManager.Instance.LV[(int)Define.LV.ShootSpeed] < 4)
            Obj.SetActive(true);
    }


}
