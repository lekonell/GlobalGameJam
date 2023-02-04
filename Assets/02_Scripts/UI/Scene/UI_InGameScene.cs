using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_InGameScene : UI_Scene
{
    public Sc_PlayerWeaponImage weaponImage;

    public string currentMapType = "A";
    public bool isTest = true;
    enum Images
    {
        HP1, HP2, HP3, HP4, HP5,
        UIImages,
        WeaponImage,
    }

    enum Buttons
    {
        TestNextButton,
        TestHit,
        TestGetGold,
        TestChangeWeapon,
    }

    enum Texts
    {
        GoldText,
    }



    // Start is called before the first frame update
    void Awake()
    {
        base.Init(); // UI_Button 의 부모인 UI_PopUp 의 Init() 호출

        Bind<Button>(typeof(Buttons)); // 버튼 오브젝트들 가져와 dictionary인 _objects에 바인딩. 
        Bind<Image>(typeof(Images)); // 버튼 오브젝트들 가져와 dictionary인 _objects에 바인딩. 
        Bind<Text>(typeof(Texts)); // 버튼 오브젝트들 가져와 dictionary인 _objects에 바인딩. 

        GetButton((int)Buttons.TestNextButton).gameObject.BindEvent(Managers.GM.OnPortal);
        GetButton((int)Buttons.TestHit).gameObject.BindEvent(OnClickTestHit);
        GetButton((int)Buttons.TestGetGold).gameObject.BindEvent(OnClickTestGetGold);
        GetButton((int)Buttons.TestChangeWeapon).gameObject.BindEvent(OnClickChangeWeapon);


        currentMapType = SceneManager.GetActiveScene().name.Substring(0, SceneManager.GetActiveScene().name.Length - 1);

        weaponImage = Managers.Resource.Load<Sc_PlayerWeaponImage>("Scriptable/PlayerWeaponImage");


        if (!isTest)
        {
            GetButton((int)Buttons.TestNextButton).gameObject.SetActive(false);
            GetButton((int)Buttons.TestHit).gameObject.SetActive(false);
            GetButton((int)Buttons.TestGetGold).gameObject.SetActive(false);
            GetButton((int)Buttons.TestChangeWeapon).gameObject.SetActive(false);
        }

        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Managers.UI.ShowPopupUI<UI_InGamePause>();
        }

    }


    public override void UpdateUI()
    {
        switch (Managers.TempPlayer.currentWeapon)
        {
            case "무기1":
                GetImage((int)Images.WeaponImage).sprite = weaponImage.weaponA;
                break;
            case "무기2":
                GetImage((int)Images.WeaponImage).sprite = weaponImage.weaponB;
                break;
            default:
                break;
        }

        GetImage((int)Images.WeaponImage).GetComponentInChildren<Text>().text = Managers.TempPlayer.currentWeapon;
        GetText((int)Texts.GoldText).GetComponent<Text>().text = $"{Managers.TempPlayer.gold}";

        GetImage((int)Images.HP1).enabled = false;
        GetImage((int)Images.HP2).enabled = false;
        GetImage((int)Images.HP3).enabled = false;
        GetImage((int)Images.HP4).enabled = false;
        GetImage((int)Images.HP5).enabled = false;


        switch (Managers.TempPlayer.hp)
        {
            case 1:
                GetImage((int)Images.HP1).enabled = true;
                break;
            case 2:
                GetImage((int)Images.HP1).enabled = true;
                GetImage((int)Images.HP2).enabled = true;
                break;
            case 3:
                GetImage((int)Images.HP1).enabled = true;
                GetImage((int)Images.HP2).enabled = true;
                GetImage((int)Images.HP3).enabled = true;
                break;
            case 4:
                GetImage((int)Images.HP1).enabled = true;
                GetImage((int)Images.HP2).enabled = true;
                GetImage((int)Images.HP3).enabled = true;
                GetImage((int)Images.HP4).enabled = true;
                break;
            case 5:
                GetImage((int)Images.HP1).enabled = true;
                GetImage((int)Images.HP2).enabled = true;
                GetImage((int)Images.HP3).enabled = true;
                GetImage((int)Images.HP4).enabled = true;
                GetImage((int)Images.HP5).enabled = true;
                break;
            default:
                break;
        }
    }

    void OnClickTestGetGold(PointerEventData data = default)
    {
        print("골드");
        Managers.TempPlayer.gold++;
        UpdateUI();
    }

    void OnClickTestHit(PointerEventData data = default)
    {
        Managers.TempPlayer.hp--;
        if (Managers.TempPlayer.hp == 0)
            print("다이");
        UpdateUI();
    }

    void OnClickChangeWeapon(PointerEventData data = default)
    {
        if (Managers.TempPlayer.currentWeapon == "무기1")
            Managers.TempPlayer.currentWeapon = "무기2";
        else
            Managers.TempPlayer.currentWeapon = "무기1";



        UpdateUI();
    }
}