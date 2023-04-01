using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedAndMoney : MonoBehaviour
{
    [SerializeField]
    GameObject LVImage;

    TextMeshProUGUI ShowUpgradeMoneyText;

    private void Start()
    {
        ShowUpgradeMoneyText = transform.Find("NeedMoney").GetComponent<TextMeshProUGUI>();
    }
    public void UpgradeMoneyGet()
    {

        if (GameManager.Instance.LV[(int)Define.LV.MoneyGet] < 4 && (GameManager.Instance.Money >= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.MoneyGet]]))
        {
            GameManager.Sound.Play("Effect/levelup1");

            GameManager.Instance.Money -= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.MoneyGet]];
            GameManager.Instance.LV[(int)Define.LV.MoneyGet] += 1;

            GameManager.UI.PointUpdate();

            LVImage.GetComponent<Image>().sprite = GameManager.UI.LVImage[GameManager.Instance.LV[(int)Define.LV.MoneyGet]]; // 
            ShowUpgradeMoneyText.text = $"{GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.MoneyGet]]}";

            gameObject.SetActive(false);
        }
    }

    public void UpgradeShootSpeed()
    {
        if (GameManager.Instance.LV[(int)Define.LV.ShootSpeed] < 4 && (GameManager.Instance.Money >= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.ShootSpeed]]))
        {
            GameManager.Sound.Play("Effect/levelup1");

            GameManager.Instance.Money -= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.ShootSpeed]];
            GameManager.Instance.LV[(int)Define.LV.ShootSpeed] += 1;

            GameManager.UI.PointUpdate();

            LVImage.GetComponent<Image>().sprite = GameManager.UI.LVImage[GameManager.Instance.LV[(int)Define.LV.ShootSpeed]]; // 
            ShowUpgradeMoneyText.text = $"{GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.ShootSpeed]]}";

            gameObject.SetActive(false);
        }
    }
}
