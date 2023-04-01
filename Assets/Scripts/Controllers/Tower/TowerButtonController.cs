using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using TMPro;


public class TowerButtonController : EventTriggerEX
{
    [SerializeField]
    Define.Property MyProperty = Define.Property.Fire;
    //EventTrigger eventTrigger;
    
    GameObject UiImage;
    GameObject UiDragImage;
    
    GameObject ShowUpgradeMoney;
    bool CanBuild = false;
    
    private void Start()
    {
        UiDragImage = new GameObject();
        UiImage = Resources.Load<GameObject>($"Prefabs/UI/{System.Enum.GetName(typeof(Define.Property), MyProperty)}T_Drag_UI");

        ShowUpgradeMoney = transform.Find($"{transform.name}_Plus").gameObject;
        init();
       
    }
    

    protected override void OnPointerExit(PointerEventData data)
    {
        //TODO
    }
    protected override void OnPointerDown(PointerEventData data)
    {
        //TODO
    }

    protected override void OnDrag(PointerEventData data)
    {
        UiDragImage.transform.position = data.position;
    }

    protected override void OnEndDrag(PointerEventData data)
    {
        //TODO
        Destroy(UiDragImage);
        UiDragImage = new GameObject();
        if (CanBuild)
        {
            GameObject tile = GameManager.Input.GetClicked2DObject(1 << 7);
            if (tile != null && (GameManager.Instance.Money >= 20))
            {
                if (tile.transform.GetComponent<Tile_Controller>().TowerNum == 0)
                {
                    tile.transform.GetComponent<Tile_Controller>().TowerNum += 1;
                    GameManager.Instance.Money -= 20;
                    GameManager.UI.PointUpdate();
                    tile.GetComponent<Tile_Controller>().InstanceTower(MyProperty);
                    GameManager.Sound.Play("Effect/tower_getto_daze");
                }
            }
        }
        CanBuild = false;
    }

    protected override void OnPointerClick(PointerEventData data)
    {
        //TODO
        if (GameManager.Instance.LV[(int)MyProperty] < 4)
        {
            GameManager.Sound.Play("Effect/button2");

            ShowUpgradeMoney.gameObject.SetActive(true);
        }
    }

    protected override void OnBeginDrag(PointerEventData data)
    {
        if (GameManager.Instance.Money >= 20)
        {
            UiDragImage = Instantiate(UiImage, this.transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
            CanBuild = true;
        }
    }
    

}
