using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using TMPro;
using System;

public class SkillButtonController : EventTriggerEX
{
    [SerializeField]
    Define.Skill MySkill = Define.Skill.Explosion;
    
    GameObject UiImage;
    GameObject UiDragImage;

    GameObject ShowUpgradeMoney;

    readonly int[] _skillConst = new int[(int)Define.Skill.MaxCount];
    

    bool CanBuild = false;
    private void Start()
    {
        init();
        _skillConst[(int)Define.Skill.Explosion] = 20;
        _skillConst[(int)Define.Skill.Sticky] = 20;
        _skillConst[(int)Define.Skill.Nullity] = 20;
        
        UiImage = Resources.Load<GameObject>($"Prefabs/UI/{System.Enum.GetName(typeof(Define.Skill), MySkill)}_Drag_UI");
        
        ShowUpgradeMoney = transform.Find($"{transform.name}_Plus").gameObject;
    }
   protected override void OnBeginDrag(PointerEventData data)
    {
        if (GameManager.Instance.Money >= _skillConst[(int)MySkill])
        {
            UiDragImage = Instantiate(UiImage, this.transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
            CanBuild = true;
        }
    }
    protected override void OnDrag(PointerEventData data)
    {
        if (CanBuild)
        {
            UiDragImage.transform.position = data.position;
        }
    }

    protected override void OnEndDrag(PointerEventData data)
    {
        if (UiDragImage != null)
        {
            Destroy(UiDragImage);
        }
        
        //UiDragImage = new GameObject();
        
        if (CanBuild)
        {
            GameObject tile = GameManager.Input.GetClicked2DObject(((1 << 7) + (1 << 8 )));

            if (tile != null && (GameManager.Instance.Money >= _skillConst[(int)MySkill]))
            {
                GameManager.Instance.Money -= _skillConst[(int)MySkill];
                GameManager.UI.PointUpdate();
                tile.GetComponent<Tile_Controller>().InstanceSkill(MySkill);
                GameManager.Sound.Play("Effect/button2");


            }

        }
        CanBuild = false;


    }

    protected override void OnPointerClick(PointerEventData data)
    {
        if (GameManager.Instance.LV[(int)MySkill + 5] < 4)
        {

            Debug.Log("Plus Ű");
            GameManager.Sound.Play("Effect/button2");

            ShowUpgradeMoney.gameObject.SetActive(true);
        }
    }



}
