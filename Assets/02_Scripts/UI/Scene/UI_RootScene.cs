using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UI_RootScene : UI_Scene
{
    enum GameObjects
    {
        Player,

        Point1,
        Point2,
        Point3,
        Point4,
        Point5,
    }

    enum Buttons
    {
        TestButton,
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Init(); // UI_Button 의 부모인 UI_PopUp 의 Init() 호출

        Bind<GameObject>(typeof(GameObjects)); // 버튼 오브젝트들 가져와 dictionary인 _objects에 바인딩. 

        GetObject((int)GameObjects.Player).transform.position = GetObject((int)GameObjects.Point1 + Managers.GM.currentFloor).transform.position;

        StartCoroutine(CoNextStap());
    }

    public float moveTime = 5;
    bool isMove = false;
    public float sceneChangeDelayTime = 2f;
    IEnumerator CoNextStap()
    {
        yield return new WaitForSeconds(Managers.Scene.WAIT_TIME + Managers.Scene.FADE_TIME);
        isMove = true;
        float currentDelta = 0;
        while (currentDelta / moveTime < 1)
        {
            Vector3 movePos = Vector3.Lerp(
                GetObject((int)GameObjects.Point1 + Managers.GM.currentFloor).transform.position,
                GetObject((int)GameObjects.Point1 + Managers.GM.currentFloor + 1).transform.position,
                currentDelta / moveTime);
            GetObject((int)GameObjects.Player).transform.position = movePos;

            currentDelta += Time.deltaTime;
            yield return 0;
        }
        yield return new WaitForSeconds(sceneChangeDelayTime);
        Managers.GM.currentFloor++;

        isMove = false;
        if (Managers.GM.currentFloor == 1 || Managers.GM.currentFloor == 2)
            Managers.Scene.LoadScene(Define.Scene.InGameA, Managers.Scene.changeSceneDelay);
        else
            Managers.Scene.LoadScene(Define.Scene.InGameB, Managers.Scene.changeSceneDelay);
        Managers.UI.ShowPopupUI<UI_ShowLoading>();
    }

    void OnClick(PointerEventData data = default)
    {
        print("클릭");
    }
}