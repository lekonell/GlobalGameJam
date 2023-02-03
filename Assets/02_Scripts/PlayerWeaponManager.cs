using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour {
	public enum ePlayerWeaponType {
		WeaponTypeMelee,
		WeaponTypeRange
	};

	private ePlayerWeaponType weaponType = ePlayerWeaponType.WeaponTypeMelee;
	private float weaponCooldown = 1.0f;

	public PlayerWeaponManager SetWeaponType(ePlayerWeaponType _weaponType) {
		weaponType = _weaponType;
		return this;
	}

	public PlayerWeaponManager SetWeaponCooldown(float _weaponCooldown) {
		weaponCooldown = _weaponCooldown;
		return this;
	}


}