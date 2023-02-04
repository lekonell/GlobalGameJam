using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        if (!Managers.Sound.CheckBgmPlay(Managers.Sound.bgmSound.mainBgm))
            Managers.Sound.Play(Managers.Sound.bgmSound.mainBgm, Define.Sound.Bgm);
        SceneType = Define.Scene.Credit;

        Managers.UI.ShowSceneUI<UI_CreditScene>();
        Managers.UI.ShowPopupUI<UI_HideLoading>();
    }

    public override void Clear()
    {
        
    }
}
