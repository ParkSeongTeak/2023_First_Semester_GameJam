using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Delete_Tower : MonoBehaviour
{
    [SerializeField]
    GameObject _deleteButtonPrefab;

    static Action Action;
    bool MouseDownMe = false;
    void Start()
    {
        Action += DeleteBtnTrue_False;
        _deleteButtonPrefab.GetComponent<DeleteTower_Controller>().GetTrigger(this);

    }

    private void OnMouseDown()
    {
        MouseDownMe = true;
        Action.Invoke();
    }
    
    void DeleteBtnTrue_False()
    {
        if (MouseDownMe)
        {
            _deleteButtonPrefab.SetActive(true);
            MouseDownMe = false;
        }
        else
        {
            _deleteButtonPrefab.SetActive(false);
        }
    }
    public void DeleteAction()
    {
        Action -= DeleteBtnTrue_False;
    }
}
