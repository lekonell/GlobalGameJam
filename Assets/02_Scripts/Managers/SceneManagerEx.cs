using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public readonly float FADE_TIME = 0.1f;
    public readonly float WAIT_TIME = 0.5f;
    public float changeSceneDelay = 0.5f;

    public int InGameACount = 3;
    public int InGameBCount = 3;

    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene type, float delayTime = 0f)
    {
        //Managers.Clear();
        Managers.Instance.StartCoroutine(CoLoadScene(type, delayTime));
    }

    IEnumerator CoLoadScene(Define.Scene type, float delayTime = 0f)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(GetSceneName(type));
    }

    public string GetSceneName(Define.Scene type)
    {
        string name;
        if (type == Define.Scene.InGameA)
            name = System.Enum.GetName(typeof(Define.Scene), type) + Random.Range(0, InGameACount);
        else if (type == Define.Scene.InGameB)
            name = System.Enum.GetName(typeof(Define.Scene), type) + Random.Range(0, InGameBCount);
        else
            name = System.Enum.GetName(typeof(Define.Scene), type);

        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
