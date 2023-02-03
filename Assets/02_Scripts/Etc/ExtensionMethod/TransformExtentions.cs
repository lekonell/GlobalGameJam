using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtentions
{
    /// <summary>
    /// 두 번째 인자가 true면 글로벌 좌표로 변경, false면 로컬 좌표로 변경됩니다.
    /// </summary>
    public static void SetX(this Transform tr, float value, bool GlobalPos = true)
    {
        Vector3 v = new Vector3(value, tr.position.y, tr.position.z);
        if (GlobalPos)
            tr.position = v;
        else
            tr.localPosition = v;
    }

    public static void SetY(this Transform tr, float value, bool GlobalPos = true)
    {
        Vector3 v = new Vector3(tr.position.x, value, tr.position.z);
        if (GlobalPos)
            tr.position = v;
        else
            tr.localPosition = v;
    }

    public static void SetZ(this Transform tr, float value, bool GlobalPos = true)
    {
        Vector3 v = new Vector3(tr.position.x, tr.position.y, value);
        if (GlobalPos)
            tr.position = v;
        else
            tr.localPosition = v;
    }
}