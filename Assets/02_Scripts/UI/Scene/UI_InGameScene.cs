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
        // GetButton((int)Buttons.TestHit).gameObject.BindEvent(OnClickTestHit);
        //GetButton((int)Buttons.TestGetGold).gameObject.BindEvent(OnClickTestGetGold);
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
            if (Managers.UI.currentPopup == null || Managers.UI.currentPopup.GetType() != typeof(UI_InGamePause))
            {
                Time.timeScale = 0;
                Managers.UI.ShowPopupUI<UI_InGamePause>();
            }
        }
    }


    public override void UpdateUI()
    {

        PlayerWeaponManager playerWeaponManager = Camera.main.GetComponent<CameraManager>().player.GetComponent<PlayerControl>().playerWeaponManager;
        if (playerWeaponManager != null)
        {
            int playerWeaponType = (int)playerWeaponManager.GetWeaponType();
            switch (playerWeaponType)
            {
                case 0: // 근접
                    GetImage((int)Images.WeaponImage).sprite = weaponImage.weaponSword;
                    break;
                case 1: // 원거리
                    GetImage((int)Images.WeaponImage).sprite = weaponImage.weaponBow;
                    break;
                default:
                    break;
            }
        }



        GetImage((int)Images.HP1).enabled = false;
        GetImage((int)Images.HP2).enabled = false;
        GetImage((int)Images.HP3).enabled = false;
        GetImage((int)Images.HP4).enabled = false;
        GetImage((int)Images.HP5).enabled = false;

        int playerHP = (int)Camera.main.GetComponent<CameraManager>().player.GetComponent<PlayerControl>().GetPlayerHP();

        switch (playerHP)
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

	public List<Sprite> goldSprites;
	public int[] goldSpritesIndex = new int[2] { 0, 0 };
	public bool isGoldSpritesInited = false;

	private GameObject digitWrapper = null;
	private GameObject[] digits = new GameObject[2] { null, null };

	public void PreloadGoldSprites() {
		if (isGoldSpritesInited)
			return;

		/*
		 * 00-07: 0->1 | 8
		 * 07-14: 1->2 | 8
		 * 14-21: 2->3 | 8
		 * 21-28: 3->4 | 8
		 * 28-35: 4->5 | 8
		 * 36-42: 5->6 | 8
		 * 42-48: 6->7 | 7
		 * 48-55: 7->8 | 8
		 * 55-62: 8->9 | 8
		 * 62-69: 9->0 | 8
		 */

		digitWrapper = GameObject.Find("DigitWrapper");
		digits[0] = digitWrapper.transform.Find("Digit_1").gameObject;
		digits[1] = digitWrapper.transform.Find("Digit_2").gameObject;

		goldSprites = new List<Sprite>();
		int[] spriteCounts = new int[10] { 8, 8, 8, 8, 8, 8, 7, 7, 8, 8 };
		goldSprites.AddRange(Resources.LoadAll<Sprite>("/GoldSprites/"));

		isGoldSpritesInited = true;
	}

	public void UpdateGold(int goldFrom, int goldTo) {
		PreloadGoldSprites();
		// int playerGold = Camera.main.GetComponent<CameraManager>().player.GetComponent<PlayerControl>().GetPlayerGold();

		string sGoldFrom = goldFrom.ToString();
		string sGoldTo = goldTo.ToString();

		for (int i = 0; i < 2; i++) {
			if (sGoldFrom.Substring(i, 1) == sGoldTo.Substring(i, 1))
				continue;

			int diff = int.Parse(sGoldTo.Substring(i, 1)) - int.Parse(sGoldFrom.Substring(i, 1));

			// StartCoroutine(UpdateGoldProcess(i, );
		}
	}

	public IEnumerator UpdateGoldProcess(int idx, int digitFrom, int digitTo) {
		int currentDigit = digitFrom;
		while (currentDigit <= digitTo) {
			// digits[0].GetComponent<Image>().sprite = goldSprites[];
			yield return new WaitForSeconds(0.13f);
		}
	}

	//void OnClickTestGetGold(PointerEventData data = default)
	//{
	//    Managers.TempPlayer.gold++;
	//    UpdateUI();
	//}

	public void UpdateUI_PlayerDie(PointerEventData data = default)
    {
        int playerHP = (int)Camera.main.GetComponent<CameraManager>().player.GetComponent<PlayerControl>().GetPlayerHP();

        if (playerHP == 0)
        {
            Managers.UI.ShowPopupUI<UI_ShowLoading>();
            Managers.Scene.LoadScene(Define.Scene.Main, Managers.Scene.changeSceneDelay);
        }

        UpdateUI();
    }

    void OnClickChangeWeapon(PointerEventData data = default)
    {
        //if (Managers.TempPlayer.currentWeapon == "무기1")
        //    Managers.TempPlayer.currentWeapon = "무기2";
        //else
        //    Managers.TempPlayer.currentWeapon = "무기1";



        UpdateUI();
    }
}