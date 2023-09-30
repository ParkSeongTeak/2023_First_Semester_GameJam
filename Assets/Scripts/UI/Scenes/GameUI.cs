using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI : UI_Scene
{
    enum Images 
    { 
        Life0, 
        Life1, 
        Life2, 
        Life3, 
        Life4,
        Tower_Fire_Plus,
        Tower_Water_Plus,
        Tower_Grass_Plus,
        Skill_Explosion_Plus,
        Skill_Slow_Plus,
        Skill_Neutralize_Plus,
        AttackSpeed_Plus,
        Money_Plus,

        Tower_Fire_LV,
        Tower_Water_LV,
        Tower_Grass_LV,
        Skill_Explosion_LV,
        Skill_Slow_LV,
        Skill_Neutralize_LV,
        AttackSpeed_LV,
        Money_LV,

    }

    enum Buttons
    {
        Tower_Fire,
        Tower_Water,
        Tower_Grass,
        Skill_Explosion,
        Skill_Slow,
        Skill_Neutralize,
        AttackSpeed,
        Money,
        Help,
        Option,
        BG_Btn
    }

    enum Texts
    {
        MoneyPoint,
        MaxPoint,
        NowPoint,
        WaveNum,
        Tower_FireTxt,
        Tower_WaterTxt,
        Tower_GrassTxt,
        Skill_ExplosionTxt,
        Skill_SlowTxt,
        Skill_NeutralizeTxt,
        AttackSpeedTxt,
        MoneyTxt,
    }

    Sprite fullLife;
    Sprite emptyLife;
    
    public override void Init()
    {
        base.Init();
        
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));

        fullLife = GameManager.Resource.Load<Sprite>("Sprite/UI/3_체력O");
        emptyLife = GameManager.Resource.Load<Sprite>("Sprite/UI/3_체력X");

        Bind_Btn();
        UpdateData();
    }


    #region Btn

    GameObject UiDragImage;
    GameObject UiImage;
    readonly int[] _skillConst = new int[(int)Define.Skills.MaxCount];
    bool CanBuild = false;

    bool[] toggle_Skill = new bool[3];
    bool[] toggle_Tower = new bool[3];

    bool toggle_AttackSpeed = false;
    bool toggle_Money = false;


    void Bind_Btn()
    {
        _skillConst[(int)Define.Skills.Explosion] = 20;
        _skillConst[(int)Define.Skills.Slow] = 20;
        _skillConst[(int)Define.Skills.Neutralize] = 20;

        BindEvent(GetButton((int)Buttons.Tower_Fire).gameObject, Btn_Tower_Fire_Click);
        BindEvent(GetButton((int)Buttons.Tower_Fire).gameObject, Btn_Tower_Fire_DragStart, Define.UIEvent.DragStart);
        BindEvent(GetButton((int)Buttons.Tower_Fire).gameObject, Btn_Tower_Fire_Drag, Define.UIEvent.Drag);
        BindEvent(GetButton((int)Buttons.Tower_Fire).gameObject, Btn_Tower_Fire_DragEnd, Define.UIEvent.DragEnd);

        BindEvent(GetButton((int)Buttons.Tower_Water).gameObject, Btn_Tower_Water_Click);
        BindEvent(GetButton((int)Buttons.Tower_Water).gameObject, Btn_Tower_Water_DragStart, Define.UIEvent.DragStart);
        BindEvent(GetButton((int)Buttons.Tower_Water).gameObject, Btn_Tower_Water_Drag, Define.UIEvent.Drag);
        BindEvent(GetButton((int)Buttons.Tower_Water).gameObject, Btn_Tower_Water_DragEnd, Define.UIEvent.DragEnd);

        BindEvent(GetButton((int)Buttons.Tower_Grass).gameObject, Btn_Tower_Grass_Click);
        BindEvent(GetButton((int)Buttons.Tower_Grass).gameObject, Btn_Tower_Grass_DragStart, Define.UIEvent.DragStart);
        BindEvent(GetButton((int)Buttons.Tower_Grass).gameObject, Btn_Tower_Grass_Drag, Define.UIEvent.Drag);
        BindEvent(GetButton((int)Buttons.Tower_Grass).gameObject, Btn_Tower_Grass_DragEnd, Define.UIEvent.DragEnd);



        BindEvent(GetButton((int)Buttons.Skill_Explosion).gameObject, Btn_Skill_Explosion_Click);
        BindEvent(GetButton((int)Buttons.Skill_Explosion).gameObject, Btn_Skill_Explosion_DragStart, Define.UIEvent.DragStart);
        BindEvent(GetButton((int)Buttons.Skill_Explosion).gameObject, Btn_Skill_Explosion_Drag, Define.UIEvent.Drag);
        BindEvent(GetButton((int)Buttons.Skill_Explosion).gameObject, Btn_Skill_Explosion_DragEnd, Define.UIEvent.DragEnd);

        BindEvent(GetButton((int)Buttons.Skill_Slow).gameObject, Btn_Skill_Slow_Click);
        BindEvent(GetButton((int)Buttons.Skill_Slow).gameObject, Btn_Skill_Slow_DragStart, Define.UIEvent.DragStart);
        BindEvent(GetButton((int)Buttons.Skill_Slow).gameObject, Btn_Skill_Slow_Drag, Define.UIEvent.Drag);
        BindEvent(GetButton((int)Buttons.Skill_Slow).gameObject, Btn_Skill_Slow_DragEnd, Define.UIEvent.DragEnd);

        BindEvent(GetButton((int)Buttons.Skill_Neutralize).gameObject, Btn_Skill_Neutralize_Click);
        BindEvent(GetButton((int)Buttons.Skill_Neutralize).gameObject, Btn_Skill_Neutralize_DragStart, Define.UIEvent.DragStart);
        BindEvent(GetButton((int)Buttons.Skill_Neutralize).gameObject, Btn_Skill_Neutralize_Drag, Define.UIEvent.Drag);
        BindEvent(GetButton((int)Buttons.Skill_Neutralize).gameObject, Btn_Skill_Neutralize_DragEnd, Define.UIEvent.DragEnd);


        BindEvent(GetButton((int)Buttons.AttackSpeed).gameObject, Btn_AttackSpeed);
        BindEvent(GetButton((int)Buttons.Money).gameObject, Btn_Money);

        BindEvent(GetButton((int)Buttons.Help).gameObject, Btn_Help);
        BindEvent(GetButton((int)Buttons.Option).gameObject, Btn_Option);
        BindEvent(GetButton((int)Buttons.BG_Btn).gameObject, Btn_BG);

        
        for (int i = 0; i < 3; i++)
        {
            toggle_Skill[i] = false;
            toggle_Tower[i] = false;
            GetImage((int)Images.Tower_Fire_Plus + i).gameObject.SetActive(false);
            GetImage((int)Images.Skill_Explosion_Plus + i).gameObject.SetActive(false);
        }

        GetImage((int)Images.AttackSpeed_Plus).gameObject.SetActive(false);
        GetImage((int)Images.Money_Plus).gameObject.SetActive(false);


    }

    void Btn_Tower_Fire_Click(PointerEventData data) 
    {
        _Click(Define.Properties.Fire);
    }

    void Btn_Tower_Fire_DragStart(PointerEventData data)
    {
        Tower_DragStart(Define.Properties.Fire);
    }
    void Btn_Tower_Fire_Drag(PointerEventData data) 
    { 
        Tower_Drag(data);

    }
    void Btn_Tower_Fire_DragEnd(PointerEventData data) 
    { 
        Tower_DragEnd(data, Define.Properties.Fire);
    }



    void Btn_Tower_Water_Click(PointerEventData data) 
    {

        _Click(Define.Properties.Water);
    }
    void Btn_Tower_Water_DragStart(PointerEventData data)
    { 
        Tower_DragStart(Define.Properties.Water);

    }
    void Btn_Tower_Water_Drag(PointerEventData data) 
    {
        Tower_Drag(data);

    }
    void Btn_Tower_Water_DragEnd(PointerEventData data) 
    { 
        Tower_DragEnd(data, Define.Properties.Water);

    }


    void Btn_Tower_Grass_Click(PointerEventData data) 
    {

        _Click(Define.Properties.Grass);
    }
    void Btn_Tower_Grass_DragStart(PointerEventData data) 
    {
        Tower_DragStart(Define.Properties.Grass);

    }

    void Btn_Tower_Grass_Drag(PointerEventData data) 
    {
        Tower_Drag(data);

    }

    void Btn_Tower_Grass_DragEnd(PointerEventData data) 
    {
        Tower_DragEnd(data, Define.Properties.Grass);

    }



    void Btn_Skill_Explosion_Click(PointerEventData data) 
    {

        _Click(Define.Skills.Explosion);
    }
    void Btn_Skill_Explosion_DragStart(PointerEventData data) 
    {
        Skill_DragStart(Define.Skills.Explosion);
    }

    void Btn_Skill_Explosion_Drag(PointerEventData data) 
    {
        Skill_Drag(data);
    }

    void Btn_Skill_Explosion_DragEnd(PointerEventData data) 
    {
        Skill_DragEnd(data, Define.Skills.Explosion);
    }



    void Btn_Skill_Slow_Click(PointerEventData data)
    {

        _Click(Define.Skills.Slow);
    }
    void Btn_Skill_Slow_DragStart(PointerEventData data) 
    {
        Skill_DragStart(Define.Skills.Slow);

    }
    void Btn_Skill_Slow_Drag(PointerEventData data) 
    {
        Skill_Drag(data);

    }
    void Btn_Skill_Slow_DragEnd(PointerEventData data) 
    { 
        Skill_DragEnd(data, Define.Skills.Slow);
    }


    void Btn_Skill_Neutralize_Click(PointerEventData data)
    {

        _Click(Define.Skills.Neutralize);
    }
    void Btn_Skill_Neutralize_DragStart(PointerEventData data) 
    { 
        Skill_DragStart(Define.Skills.Neutralize);
    }
    void Btn_Skill_Neutralize_Drag(PointerEventData data) 
    { 
        Skill_Drag(data);
    }
    void Btn_Skill_Neutralize_DragEnd(PointerEventData data) 
    {
        Skill_DragEnd(data, Define.Skills.Neutralize);
    }




    void Btn_AttackSpeed(PointerEventData data) 
    {
        if (toggle_AttackSpeed)
        {
            int AttackSpeedToLv = (int)Define.LV.AttackSpeed;
            if (GameManager.Data.LV[AttackSpeedToLv] < 4 && (GameManager.Data.Money >= GameManager.Data.UPGRATECOST[GameManager.Data.LV[AttackSpeedToLv]]))
            {
                GameManager.Data.Money -= GameManager.Data.UPGRATECOST[GameManager.Data.LV[AttackSpeedToLv]];
                GameManager.Data.LV[AttackSpeedToLv] += 1;

                GameManager.UI.ShowSceneUI<GameUI>()?.UpdateData();

                GetImage((int)Images.AttackSpeed_LV).sprite = GameManager.Resource.Load<Sprite>($"Sprite/UI/9_LV{GameManager.Data.LV[AttackSpeedToLv] + 1}");
                GetText((int)Texts.AttackSpeedTxt).text = $"{GameManager.Data.UPGRATECOST[GameManager.Data.LV[AttackSpeedToLv]]}";   

            }
        }

        toggle_AttackSpeed = !toggle_AttackSpeed;
        GetImage((int)Images.AttackSpeed_Plus).gameObject.SetActive(toggle_AttackSpeed);

    }
    void Btn_Money(PointerEventData data) 
    {
        if (toggle_Money)
        {
            int MoneyToLv = (int)Define.LV.Money;
            if (GameManager.Data.LV[MoneyToLv] < 4 && (GameManager.Data.Money >= GameManager.Data.UPGRATECOST[GameManager.Data.LV[MoneyToLv]]))
            {
                GameManager.Data.Money -= GameManager.Data.UPGRATECOST[GameManager.Data.LV[MoneyToLv]];
                GameManager.Data.LV[MoneyToLv] += 1;

                GameManager.UI.ShowSceneUI<GameUI>()?.UpdateData();

                GetImage((int)Images.Money_LV).sprite = GameManager.Resource.Load<Sprite>($"Sprite/UI/9_LV{GameManager.Data.LV[MoneyToLv] + 1}");
                GetText((int)Texts.MoneyTxt).text = $"{GameManager.Data.UPGRATECOST[GameManager.Data.LV[MoneyToLv]]}";

            }
        }

        toggle_Money = !toggle_Money;
        GetImage((int)Images.Money_Plus).gameObject.SetActive(toggle_Money);

    }
    void Btn_Help(PointerEventData data) 
    {
        GameManager.UI.ShowPopupUI<Help>();

    }

    void Btn_BG(PointerEventData data)
    {
        for(int i = 0; i < 3; i++)
        {

            toggle_Tower[i] = false;
            GetImage((int)Images.Tower_Fire_Plus + i).gameObject.SetActive(toggle_Tower[i]);

            toggle_Skill[i] = false;
            GetImage((int)Images.Skill_Explosion_Plus + i).gameObject.SetActive(toggle_Skill[i]);

        }

        toggle_AttackSpeed = false;
        GetImage((int)Images.AttackSpeed_Plus).gameObject.SetActive(toggle_AttackSpeed);


        toggle_Money = false;
        GetImage((int)Images.Money_Plus).gameObject.SetActive(toggle_Money);

    }
    void Btn_Option(PointerEventData data) 
    {
        GameManager.UI.ShowPopupUI<Option>();
    }


    void _Click(Define.Skills skill)
    {
        if (toggle_Skill[(int)skill])
        {
            int SkillToLv = (int)skill + (int)Define.LV.Explosion;
            if (GameManager.Data.LV[SkillToLv] < 4 && (GameManager.Data.Money >= GameManager.Data.UPGRATECOST[GameManager.Data.LV[SkillToLv]]))
            {
                GameManager.Data.Money -= GameManager.Data.UPGRATECOST[GameManager.Data.LV[SkillToLv]];
                GameManager.Data.LV[SkillToLv] += 1;

                UpdateData();

                GetImage((int)Images.Skill_Explosion_LV + (int)skill).sprite = GameManager.Resource.Load<Sprite>($"Sprite/UI/9_LV{GameManager.Data.LV[SkillToLv] + 1}");
                GetText((int)Texts.Skill_ExplosionTxt + (int)skill).text = $"{GameManager.Data.UPGRATECOST[GameManager.Data.LV[SkillToLv]]}";
                
            }
        }
        toggle_Skill[(int)skill] = !toggle_Skill[(int)skill];
        GetImage((int)Images.Skill_Explosion_Plus + (int)skill).gameObject.SetActive(toggle_Skill[(int)skill]);
        
    }


    void _Click(Define.Properties tower)
    {

        if (toggle_Tower[(int)tower])
        {
            int TowerToLv = (int)tower + (int)Define.LV.Fire;
            if (GameManager.Data.LV[TowerToLv] < 4 && (GameManager.Data.Money >= GameManager.Data.UPGRATECOST[GameManager.Data.LV[TowerToLv]]))
            {
                GameManager.Sound.Play("Effect/levelup1");

                GameManager.Data.Money -= GameManager.Data.UPGRATECOST[GameManager.Data.LV[TowerToLv]];
                GameManager.Data.LV[TowerToLv] += 1;

                UpdateData();

                GetImage((int)Images.Tower_Fire_LV + (int)tower).sprite = GameManager.Resource.Load<Sprite>($"Sprite/UI/9_LV{GameManager.Data.LV[TowerToLv] + 1}");
                GetText((int)Texts.Tower_FireTxt + (int)tower).text = $"{GameManager.Data.UPGRATECOST[GameManager.Data.LV[TowerToLv]]}";

            }
        }

        toggle_Tower[(int)tower] = !toggle_Tower[(int)tower];
        GetImage((int)Images.Tower_Fire_Plus + (int)tower).gameObject.SetActive(toggle_Tower[(int)tower]);

    }
    void Skill_DragStart(Define.Skills skill)
    {
        if (GameManager.Data.Money >= _skillConst[(int)skill])
        {
            UiImage = GameManager.Resource.Load<GameObject>($"Prefabs/UI/{System.Enum.GetName(typeof(Define.Skills),skill)}_Drag_UI");
            UiDragImage = Instantiate(UiImage, this.transform.position, Quaternion.identity, transform);
            CanBuild = true;
        }
    }

    void Skill_Drag(PointerEventData data)
    {
        if (CanBuild)
        {
            UiDragImage.transform.position = data.position;
        }
    }

    void Skill_DragEnd(PointerEventData data, Define.Skills skill)
    {
        if (UiDragImage != null)
        {
            Destroy(UiDragImage);
        }

        if (CanBuild)
        {
            GameObject tile = GameManager.Input.GetClicked2DObject(((1 << 7) + (1 << 8)));

            if (tile != null && (GameManager.Data.Money >= _skillConst[(int)skill]))
            {
                GameManager.Data.Money -= _skillConst[(int)skill];
                GameManager.UI.ShowSceneUI<GameUI>().UpdateData();
                tile.GetComponent<Tile_Controller>().InstanceSkill(skill);
                GameManager.Sound.Play("Effect/button2");
            }

        }
        CanBuild = false;
    }

    void Tower_DragStart(Define.Properties property)
    {
        if (GameManager.Data.Money >= 20)
        {
            UiImage = GameManager.Resource.Load<GameObject>($"Prefabs/UI/{System.Enum.GetName(typeof(Define.Properties), property)}T_Drag_UI");
            UiDragImage = Instantiate(UiImage, this.transform.position, Quaternion.identity, transform);
            CanBuild = true;
        }
    }

    void Tower_Drag(PointerEventData data)
    {
        if (CanBuild)
        {
            UiDragImage.transform.position = data.position;
        }
    }

    void Tower_DragEnd(PointerEventData data, Define.Properties property)
    {
        if (UiDragImage != null)
        {
            Destroy(UiDragImage);
        }

        if (CanBuild)
        {
            GameObject tile = GameManager.Input.GetClicked2DObject(1 << 7);

            if (tile != null && (GameManager.Data.Money >= 20))
            {
                if (tile.transform.GetComponent<Tile_Controller>().TowerNum == 0)
                {
                    tile.transform.GetComponent<Tile_Controller>().TowerNum += 1;
                    GameManager.Data.Money -= 20;
                    GameManager.UI.ShowSceneUI<GameUI>().UpdateData();
                    tile.GetComponent<Tile_Controller>().InstanceTower(property);
                    GameManager.Sound.Play("Effect/tower_getto_daze");
                }
            }
        }
        CanBuild = false;
    }

    #endregion Btn

    public void UpdateData()
    {
        GetText((int)Texts.WaveNum).text = $"{GameManager.Data.Wave} WAVE";
        GetText((int)Texts.NowPoint).text = $"SCORE: {GameManager.Data.NowPoint}";
        GetText((int)Texts.MaxPoint).text = $"BEST SCORE: {GameManager.Data.MaxPoint}";
        GetText((int)Texts.MoneyPoint).text = $"{(int)GameManager.Data.Money}";

    }

    public int Life { get; set; } = 4;
    public void MinusLife()
    {
        if (Life >= 1)
        {
            GetImage((int)Images.Life0 + Life).sprite = emptyLife;
            Life--;
        }
        else
        {
            GameManager.UI.ShowPopupUI<GameOverPopup>();
        }

    }

}
