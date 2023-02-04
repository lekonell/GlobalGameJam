using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScene : BaseScene {
	protected override void Init() {
		base.Init();

		SceneType = Define.Scene.InGameA;

		Managers.UI.ShowSceneUI<UI_TreeScene>();
		Managers.UI.ShowPopupUI<UI_HideLoading>();
	}

	public override void Clear() {

	}
}
