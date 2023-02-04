using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.InGameA;

        Managers.Resource.Instantiate("MinimapCamera");
        if (!Managers.Sound.CheckBgmPlay(Managers.Sound.bgmSound.battleBgm))
        {
            Managers.Sound.Play(Managers.Sound.bgmSound.battleBgm, Define.Sound.Bgm);
        }

        Managers.UI.ShowSceneUI<UI_InGameScene>();
        Managers.UI.ShowPopupUI<UI_HideLoading>();
    }

    public override void Clear()
    {
        
    }
}
