using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MainSceneHoverScriptor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	private Sprite originSprite;
	private Sprite hoveredSprite;

	void Start() {
		originSprite = GetComponent<Image>().sprite;
		Texture2D originTexture = originSprite.texture;

		Rect rect = new Rect(0, 0, originTexture.width, originTexture.height);

		Texture2D hoveredTexture = AdjustBrightness(originTexture, "40");
		hoveredSprite = Sprite.Create(hoveredTexture, rect, new Vector2(0.5f, 0.5f));
	}

	public void OnPointerEnter(PointerEventData eventData) {
		GetComponent<Image>().sprite = hoveredSprite;
	}

	public void OnPointerExit(PointerEventData eventData) {
		GetComponent<Image>().sprite = originSprite;
	}

	public Texture2D AdjustBrightness(Texture2D targetTexture, string brightness) {
		int brightnessInt = Convert.ToInt32(brightness);
		float mappedBrightness = (51 * brightnessInt) / 10 - 255;

		Texture2D bitmapImage = new Texture2D(targetTexture.width, targetTexture.height);

		if (mappedBrightness < -255)
			mappedBrightness = -255;
		if (mappedBrightness > 255)
			mappedBrightness = 255;

		Color color;
		for (int i = 0; i < bitmapImage.width; i++) {
			for (int j = 0; j < bitmapImage.height; j++) {
				float cR, cG, cB, cA;
				color = targetTexture.GetPixel(i, j);

				if (color.a == 1f) {
					cR = color.r + (mappedBrightness / 255);
					cG = color.g + (mappedBrightness / 255);
					cB = color.b + (mappedBrightness / 255);
					cA = color.a;


					if (cR < 0)
						cR = 0;
					if (cR > 255)
						cR = 255;

					if (cG < 0)
						cG = 0;
					if (cG > 255)
						cG = 255;

					if (cB < 0)
						cB = 0;
					if (cB > 255)
						cB = 255;
				}
				else {
					cR = color.r;
					cG = color.g;
					cB = color.b;
					cA = color.a;
				}

				bitmapImage.SetPixel(i, j, new Color(cR, cG, cB, cA));
			}
		}

		bitmapImage.Apply();

		return bitmapImage;
	}
}