using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public AudioClip testClip;
    public Sc_PlayerSound playerSound;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.InGameA;

        Managers.UI.ShowSceneUI<UI_MainScene>();
        Managers.UI.ShowPopupUI<UI_HideLoading>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Managers.Sound.Play(playerSound.A);
        }
    }

    public override void Clear()
    {

    }
}
