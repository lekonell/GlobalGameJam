using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using static UnityEditor.PlayerSettings;

public class UI_TreeScene : UI_Scene
{
    public float moveTime;

    enum GameObjects
    {
        Player,
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


        //GetObject((int)GameObjects.Player).transform.DOMove(Vector3.zero, 3);
        GetObject((int)GameObjects.Player).GetComponent<RectTransform>().DOAnchorPos(Vector3.zero, moveTime);
        Invoke("ChangeScene", moveTime);
    }

    void ChangeScene()
    {
        Managers.Scene.LoadScene(Define.Scene.Root, Managers.Scene.changeSceneDelay);
    }
}