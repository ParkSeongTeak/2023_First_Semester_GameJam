using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TestCode : MonoBehaviour
{
    TMP_InputField InputField;

    private void Start()
    {
        InputField = this.transform.GetComponent<TMP_InputField>();
    }
    public void OnEndEditStartHP()
    {
        try
        {
            GameManager.Instance.StartHP = Int32.Parse(InputField.text);

            Debug.Log($"StartHP {InputField.text}");
        }
        catch (Exception e)
        {
            Debug.Log("StartInputFail");
        }
    }
    public void OnEndEditWaveHP()
    {
        try
        {
            GameManager.Instance.WaveHPPlus = Int32.Parse(InputField.text);
            Debug.Log($"WavetHP {InputField.text}");

        }
        catch (Exception e)
        {
            Debug.Log("WaveHPInputFail");
        }
    }
}