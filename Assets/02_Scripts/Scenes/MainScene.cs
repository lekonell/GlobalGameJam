using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public Sc_BgmSound playerSound;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.InGameA;
        if(!Managers.Sound.CheckBgmPlay(Managers.Sound.bgmSound.mainBgm))
            Managers.Sound.Play(Managers.Sound.bgmSound.mainBgm, Define.Sound.Bgm);

        Managers.UI.ShowSceneUI<UI_MainScene>();
        Managers.UI.ShowPopupUI<UI_HideLoading>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
        }
    }

    public override void Clear()
    {

    }
}
