using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

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
	public int[] spriteCounts = new int[10] { 8, 8, 8, 8, 8, 8, 7, 12, 5, 7 };

	private GameObject digitWrapper = null;
	private GameObject[] digits = new GameObject[2] { null, null };
    private float digitOriginY;

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
		 * 48-57: 7->8 | 10
		 * 57-62: 8->9 | 8
		 * 62-69: 9->0 | 7
		 * 
		 * 8에서 55 (7다음 +1)
		 * 9에서 62
		 */

		digitWrapper = GameObject.Find("DigitWrapper");
		digits[0] = digitWrapper.transform.Find("Digit_1").gameObject;
		digits[1] = digitWrapper.transform.Find("Digit_2").gameObject;

        digitOriginY = digits[0].transform.position.y;

		goldSprites = new List<Sprite>();
		goldSprites.AddRange(Resources.LoadAll<Sprite>("digits/"));

        for (int i = 0; i < goldSprites.Count; i++) {
            Debug.Log("goldSprites[" + i + "]: " + goldSprites[i].name);
        }

		isGoldSpritesInited = true;
	}

	public void UpdateGold(int goldFrom, int goldTo) {
		PreloadGoldSprites();

		string sGoldFrom = goldFrom.ToString();
		string sGoldTo = goldTo.ToString();

        if (sGoldFrom.Length == 1)
            sGoldFrom = "0" + sGoldFrom;

		if (sGoldTo.Length == 1)
			sGoldTo = "0" + sGoldTo;

		for (int i = 0; i < 2; i++) {
			if (sGoldFrom.Substring(i, 1) == sGoldTo.Substring(i, 1))
				continue;

			int diff = int.Parse(sGoldTo.Substring(i, 1)) - int.Parse(sGoldFrom.Substring(i, 1));

			if (diff != 0) {
                StartCoroutine(UpdateGoldProcess(i, int.Parse(sGoldFrom.Substring(i, 1)), int.Parse(sGoldTo.Substring(i, 1))));
            }
		}
	}

	public IEnumerator UpdateGoldProcess(int digitidx, int digitFrom, int digitTo) {
		int currentDigit = digitFrom;
        int accumulatedCount = 0;

		while (currentDigit < digitTo) {
			digits[digitidx].GetComponent<Image>().sprite = goldSprites[goldSpritesIndex[digitidx] + accumulatedCount];
            accumulatedCount += 1;

            if (accumulatedCount >= spriteCounts[currentDigit]) {
				goldSpritesIndex[digitidx] += accumulatedCount - 1;

                if (goldSpritesIndex[digitidx] >= 69)
                    goldSpritesIndex[digitidx] -= 69;
                accumulatedCount = 0;
                currentDigit += 1;
            }

			yield return new WaitForSeconds(0.04f);
		}

        Debug.Log("goldSpritesIndex[" + digitidx + "]: " + goldSpritesIndex[digitidx]);

        int[] positionWeight = new int[10] { 0, 0, 1, 1, 2, 3, 3, 0, 0, 0 };

        digits[digitidx].transform.position = new Vector2(digits[digitidx].transform.position.x, digitOriginY + positionWeight[digitTo]);

        yield break;
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