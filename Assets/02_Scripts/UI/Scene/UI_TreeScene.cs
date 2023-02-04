using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UI_TreeScene : UI_Scene
{
    public float scaleControlTime = 2f;
    public float bgMoveDelayTime = 1.8f;
    public float bgMoveTime = 7f;
    public float playerMoveTime = 4f;

    enum GameObjects
    {
        Player,
        Tree,
        Mover,
    }

    enum Buttons
    {
        TestButton,
    }

    // Start is called before the first frame update

    private GameObject realPlayer = null;

    void Start()
    {
        base.Init(); // UI_Button 의 부모인 UI_PopUp 의 Init() 호출

        Bind<GameObject>(typeof(GameObjects)); // 버튼 오브젝트들 가져와 dictionary인 _objects에 바인딩. 

        realPlayer = GameObject.Find("UnitRoot");

        //GetObject((int)GameObjects.Player).transform.DOMove(Vector3.zero, 3);
        StartCoroutine(CoEvent());
    }

    bool CoStopper = false;

    IEnumerator CoEvent()
    {
        yield return new WaitForSeconds(Managers.Scene.changeSceneDelay);
        GetObject((int)GameObjects.Mover).GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1), scaleControlTime).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(bgMoveDelayTime);
        StartCoroutine(SimpleCoroutine());
        yield return new WaitForSeconds(bgMoveTime - 2.4f);
        CoStopper = true;
        GetObject((int)GameObjects.Tree).GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, 600, 0), playerMoveTime);
        GetObject((int)GameObjects.Player).GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, -50, 0), playerMoveTime);

        realPlayer.GetComponent<PlayerControl>().animator.SetFloat("RunState", 0.5f);
        realPlayer.transform.DOMove(realPlayer.transform.position, 2.6f).OnComplete(() => {
			realPlayer.GetComponent<PlayerControl>().animator.SetFloat("RunState", 0.0f);
		});

        yield return new WaitForSeconds(playerMoveTime);
        ChangeScene();
    }

    void ChangeScene()
    {
        Managers.Scene.LoadScene(Define.Scene.Root, Managers.Scene.changeSceneDelay);
    }

    private IEnumerator SimpleCoroutine()
    {
        float multiplierCoefficient = 1.0f;
        RectTransform targetRect = GetObject((int)GameObjects.Mover).GetComponent<RectTransform>();

        float elapsedTime = 0.0f;
        while (elapsedTime <= 2.0f)
        {
            if (CoStopper == true)
                yield break;
            Vector3 dest = -targetRect.anchoredPosition;
            elapsedTime += Time.deltaTime;
            multiplierCoefficient = Mathf.Pow(elapsedTime, 2);
            targetRect.transform.Translate(dest * multiplierCoefficient * Time.deltaTime);
            yield return null;
        }

        elapsedTime = 0.0f;
        while (elapsedTime <= 4.0f)
        {
            if (CoStopper == true)
                yield break;
            Vector3 dest = -targetRect.anchoredPosition / 1.3f;
            elapsedTime += Time.deltaTime;
            multiplierCoefficient = Mathf.Exp(2.0f * elapsedTime) * 4;
            targetRect.transform.Translate(dest * multiplierCoefficient * Time.deltaTime);
            yield return null;
        }

        elapsedTime = 0.0f;
        while (elapsedTime <= 1.0f)
        {
            if (CoStopper == true)
                yield break;
            Vector3 dest = -targetRect.anchoredPosition;
            elapsedTime += Time.deltaTime;
            multiplierCoefficient = Mathf.Exp(elapsedTime) * 0.1f;
            targetRect.transform.Translate(dest * multiplierCoefficient * Time.deltaTime);
            yield return null;
        }
    }
}