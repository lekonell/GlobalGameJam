using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BgmSound", menuName = "ScriptableObject/BgmSound", order = int.MaxValue)]
public class Sc_BgmSound : ScriptableObject
{
    public AudioClip mainBgm;
    public AudioClip battleBgm;
}
