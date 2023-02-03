using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/PlayerData", order = int.MaxValue)]
public class Sc_PlayerSound : ScriptableObject
{
    public AudioClip A;
    public AudioClip B;
}
