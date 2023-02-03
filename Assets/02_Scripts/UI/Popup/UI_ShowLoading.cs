using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class UI_ShowLoading : UI_Popup
{
    public readonly float FADE_TIME = 0.1f;
    public readonly float WAIT_TIME = 1f;

    public enum GameObjects
    {
        Blocker,
        LoadingText,
    }

    float waitTime;
    float fadeTime; // Fade효과 재생시간

    float startAlpha = 0f;
    float endAlpha = 1f;

    private void Awake()
    {
        waitTime = Managers.Scene.WAIT_TIME;
        fadeTime = Managers.Scene.FADE_TIME;
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        StartCoroutine(FadePlay());
    }

    IEnumerator FadePlay()
    {
        float damping = 0f;

        Color fadecolorImage = GetObject((int)GameObjects.Blocker).GetComponent<Image>().color;
        Color fadecolorText = GetObject((int)GameObjects.LoadingText).GetComponent<Text>().color;

        damping = 0f;
        fadecolorImage.a = Mathf.Lerp(startAlpha, endAlpha, damping);
        fadecolorText.a = Mathf.Lerp(startAlpha, endAlpha, damping);

        while (damping < 1)
        {
            damping += Time.deltaTime / fadeTime;
            fadecolorImage.a = Mathf.Lerp(startAlpha, endAlpha, damping);
            fadecolorText.a = Mathf.Lerp(startAlpha, endAlpha, damping);
            GetObject((int)GameObjects.Blocker).GetComponent<Image>().color = fadecolorImage;
            GetObject((int)GameObjects.LoadingText).GetComponent<Text>().color = fadecolorText;
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);
        Managers.UI.ClosePopupUI(this);
    }
}
