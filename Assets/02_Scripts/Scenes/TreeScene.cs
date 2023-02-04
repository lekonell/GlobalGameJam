using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.InGameA; 
        if (!Managers.Sound.CheckBgmPlay(Managers.Sound.bgmSound.mainBgm))
            Managers.Sound.Play(Managers.Sound.bgmSound.mainBgm, Define.Sound.Bgm);


        Managers.UI.ShowSceneUI<UI_TreeScene>();
        Managers.UI.ShowPopupUI<UI_HideLoading>();
    }

    public override void Clear()
    {

    }
}
