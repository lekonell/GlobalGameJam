using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTransform : MonoBehaviour
{
#if UNITY_EDITOR
    Vector3 addVec = new Vector3(-374, 0, 0);

    private void Reset()
    {
        transform.position += addVec;
        StartCoroutine(CoA());
    }

    private void Awake()
    {
        Debug.LogError("의미없는 컴포넌트 존재");
    }

    IEnumerator CoA()
    {
        yield return null;
        DestroyImmediate(this.GetComponent<AddTransform>());
    }

#endif
}
