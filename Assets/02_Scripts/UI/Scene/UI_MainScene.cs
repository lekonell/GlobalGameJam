using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_MainScene : UI_Scene
{
    enum Buttons
    {
        OnStartButton,
        OnTutoButton,
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Init(); // UI_Button 의 부모인 UI_PopUp 의 Init() 호출

        Bind<Button>(typeof(Buttons)); // 버튼 오브젝트들 가져와 dictionary인 _objects에 바인딩. 

        GetButton((int)Buttons.OnStartButton).gameObject.BindEvent(LoadStart);
        GetButton((int)Buttons.OnTutoButton).gameObject.BindEvent((PointerEventData data) => { print("Tuto"); });// Managers.UI.ShowPopupUI<UI_Button>(); });
    }

    private void Update()
    {
    }

    void LoadStart(PointerEventData data = null)
    {
        Managers.UI.ShowPopupUI<UI_ShowLoading>();
        Managers.Scene.LoadScene(Define.Scene.Root, Managers.Scene.changeSceneDelay);
    }

}
