using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWeaponImage", menuName = "ScriptableObject/PlayerWeaponImage", order = int.MaxValue)]
public class Sc_PlayerWeaponImage : ScriptableObject
{
    public Sprite weaponSword;
    public Sprite weaponBow;
}
