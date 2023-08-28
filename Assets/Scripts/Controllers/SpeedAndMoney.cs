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

        if (GameManager.Instance.LV[(int)Define.LV.Money] < 4 && (GameManager.Instance.Money >= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.Money]]))
        {
            GameManager.Sound.Play("Effect/levelup1");

            GameManager.Instance.Money -= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.Money]];
            GameManager.Instance.LV[(int)Define.LV.Money] += 1;
            GameManager.UI.ShowSceneUI<GameUI>().UpdateData();

            LVImage.GetComponent<Image>().sprite = GameManager.UI.LVImage[GameManager.Instance.LV[(int)Define.LV.Money]]; // 
            ShowUpgradeMoneyText.text = $"{GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.Money]]}";

            gameObject.SetActive(false);
        }
    }

    public void UpgradeShootSpeed()
    {
        if (GameManager.Instance.LV[(int)Define.LV.AttackSpeed] < 4 && (GameManager.Instance.Money >= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.AttackSpeed]]))
        {
            GameManager.Sound.Play("Effect/levelup1");

            GameManager.Instance.Money -= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.AttackSpeed]];
            GameManager.Instance.LV[(int)Define.LV.AttackSpeed] += 1;

            GameManager.UI.ShowSceneUI<GameUI>().UpdateData();

            LVImage.GetComponent<Image>().sprite = GameManager.UI.LVImage[GameManager.Instance.LV[(int)Define.LV.AttackSpeed]]; // 
            ShowUpgradeMoneyText.text = $"{GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.AttackSpeed]]}";

            gameObject.SetActive(false);
        }
    }
}
