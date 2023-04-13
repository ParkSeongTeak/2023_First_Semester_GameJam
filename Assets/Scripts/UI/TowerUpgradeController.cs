
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using TMPro;

public class TowerUpgradeController : EventTriggerEX
{

    [SerializeField]
    Define.Property MyProperty = Define.Property.Fire;

    
    GameObject LVImage;
    TextMeshProUGUI ShowUpgradeMoneyText;

    private void Start()
    {
        init();
        ShowUpgradeMoneyText = new TextMeshProUGUI();
        LVImage = transform.parent.transform.Find($"{transform.parent.name}LV").gameObject;
        if (LVImage == null)
        {
            Debug.Log($"{transform.name} : cant find LV:  {transform.parent.name}LV");
        }
        ShowUpgradeMoneyText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        ShowUpgradeMoneyText.text = $"{GameManager.UPGRATECOST[GameManager.Instance.LV[(int)MyProperty]]}";        
    }

    protected override void OnPointerDown(PointerEventData data)
    {

        if (GameManager.Instance.LV[(int)MyProperty] < 4 && (GameManager.Instance.Money >= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)MyProperty]]))
        {
            GameManager.Sound.Play("Effect/levelup1");

            GameManager.Instance.Money -= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)MyProperty]];
            GameManager.Instance.LV[(int)MyProperty] += 1;

            GameManager.UI.PointUpdate();

            LVImage.GetComponent<Image>().sprite = GameManager.UI.LVImage[GameManager.Instance.LV[(int)MyProperty]]; // 
            ShowUpgradeMoneyText.text = $"{GameManager.UPGRATECOST[GameManager.Instance.LV[(int)MyProperty]]}";

            gameObject.SetActive(false);
        }
    }


    protected override void OnEndDrag(PointerEventData data)
    {
        gameObject.SetActive(false);
    }

}
