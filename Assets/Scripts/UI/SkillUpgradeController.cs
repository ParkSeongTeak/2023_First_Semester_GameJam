using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using TMPro;


public class SkillUpgradeController : EventTriggerEX

{
    [SerializeField]
    Define.Skill MySkill = Define.Skill.Explosion;
    
    
    GameObject LVImage;
    TextMeshProUGUI ShowUpgradeMoneyText;
    bool _startBool = false;
    public bool StartBool { get{ return _startBool; } set{ _startBool = value; } }
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
        ShowUpgradeMoneyText.text = $"{GameManager.UPGRATECOST[GameManager.Instance.LV[(int)MySkill]]}";

        Debug.Log(ShowUpgradeMoneyText.name);


       
    }

    protected override void OnPointerDown(PointerEventData data)
    {
        Debug.Log("InSkillUpgrade");
        int SkillToLv = (int)MySkill + 5;
        if (GameManager.Instance.LV[SkillToLv] < 4 && (GameManager.Instance.Money >= GameManager.UPGRATECOST[GameManager.Instance.LV[SkillToLv]]))
        {
            GameManager.Instance.Money -= GameManager.UPGRATECOST[GameManager.Instance.LV[SkillToLv]];
            GameManager.Instance.LV[SkillToLv] += 1;

            GameManager.UI.PointUpdate();

            LVImage.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprite/UI/9_LV{GameManager.Instance.LV[SkillToLv] + 1}");
            ShowUpgradeMoneyText.text = $"{GameManager.UPGRATECOST[GameManager.Instance.LV[SkillToLv]]}";

            gameObject.SetActive(false);
        }

    }

    protected override void OnEndDrag(PointerEventData data)
    {
        Debug.Log("PlusRayCastTower");
        gameObject.SetActive(false);

    }

}

