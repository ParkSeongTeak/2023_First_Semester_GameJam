using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InputManager 
{
    Action _keyAction = null;
    public Action KeyAction { get { return _keyAction; } set { _keyAction = value; } }



    public void init()
    {

    }
    public void OnUpdate()
    {
        KeyAction?.Invoke();

    }


    public GameObject GetClicked2DObject(int LayerMask)
    {
        GameObject target = null;


        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray2D ray = new Ray2D(pos, Vector2.zero);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask);

        if (hit) //마우스 근처에 오브젝트가 있는지 확인
        {
            //있으면 오브젝트를 저장한다.
            target = hit.collider.gameObject;
        }
        return target;
    }

    IEnumerator Play()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
        }
    }

}
