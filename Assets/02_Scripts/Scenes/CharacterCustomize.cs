using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomize : MonoBehaviour {
	public void OnCustomizeButtonClick() {
		string clickedButton = gameObject.name.Split("btn")[1];
		string clickedPart = "";

		/*
		 * 버튼 이름은 btn[부위명][Left|Right]로 정의한다.
		 * clickedPart에 [부위명]이 저장된다.
		 */
		if (clickedButton.Substring(clickedButton.Length - 4, 4) == "Left") {
			clickedPart = clickedButton.Substring(0, clickedButton.Length - 4);
		}
		else {
			clickedPart = clickedButton.Substring(0, clickedButton.Length - 5);
		}

		switch (clickedPart) {
			case "Hair":
				break;
			case "Cloth":
				break;
			case "Armor":
				break;
		}
	}
}
