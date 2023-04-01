using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Delete_Tower : MonoBehaviour
{
    GameObject _deleteButton;

    void Start()
    {
        _deleteButton = transform.parent.Find("Delete").gameObject;

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject Tower = GameManager.Input.GetClicked2DObject(1 << 11);
            if (Tower != null && Tower == gameObject)
            {
                _deleteButton.SetActive(true);
            }
            else if (Tower == null)
            {
                _deleteButton.SetActive(false) ;
            }
        }
    }
}
