using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_InGamePause : UI_Popup
{

    enum Buttons
    {
        RestartButton, GoMainButton,
    }




    // Start is called before the first frame update
    void Awake()
    {
        base.Init(); // UI_Button 의 부모인 UI_PopUp 의 Init() 호출

        Bind<Button>(typeof(Buttons)); // 버튼 오브젝트들 가져와 dictionary인 _objects에 바인딩. 

        GetButton((int)Buttons.RestartButton).gameObject.BindEvent(OnClickRestartButton);
        GetButton((int)Buttons.GoMainButton).gameObject.BindEvent(OnClickGoMainButton);

        UpdateUI();
    }

    public override void UpdateUI()
    {
    }

    void OnClickRestartButton(PointerEventData data = default)
    {
        Time.timeScale = 1;
        Managers.UI.ClosePopupUI(this);
    }

    void OnClickGoMainButton(PointerEventData data = default)
    {
        Time.timeScale = 1;
        Managers.UI.ShowPopupUI<UI_ShowLoading>();
        Managers.Scene.LoadScene(Define.Scene.Main, Managers.Scene.changeSceneDelay);
    }
}