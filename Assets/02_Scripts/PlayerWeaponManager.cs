using GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour {
	private GameObject player = null;
	private PlayerControl.ePlayerWeaponType weaponType = PlayerControl.ePlayerWeaponType.WeaponTypeMelee;
	private float weaponCooldown;
	private float weaponDamage;

	private void Update() {
		if (player == null) {
			player = ItemManager.Find("Player");
			return;
		}
	}

	public PlayerWeaponManager SetWeaponType(PlayerControl.ePlayerWeaponType _weaponType) {
		weaponType = _weaponType;

		switch (weaponType) {
			case PlayerControl.ePlayerWeaponType.WeaponTypeMelee:
				SetWeaponDamage(54.0f);
				SetWeaponCooldown(0.6f);

				ItemManager.Find("Player/R_Weapon").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SPUM/SPUM_Sprites/Packages/Ver300/6_Weapons/New_Weapon_20");
				break;
			case PlayerControl.ePlayerWeaponType.WeaponTypeRange:
				SetWeaponDamage(36.0f);
				SetWeaponCooldown(1.0f);

				ItemManager.Find("Player/R_Weapon").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("SPUM/SPUM_Sprites/Packages/Ver300/6_Weapons/New_Weapon_10");
				break;
		}

		Managers.UI.UpdateUI();

		return this;
	}

	public PlayerWeaponManager SetWeaponCooldown(float _weaponCooldown) {
		weaponCooldown = _weaponCooldown;
		return this;
	}

	public PlayerWeaponManager SetWeaponDamage(float _weaponDamage) {
		weaponDamage = _weaponDamage * 10;
		return this;
	}

	public float GetWeaponDamage() {
		return weaponDamage;
	}

	public float GetWeaponCooldown() {
		return weaponCooldown;
	}

	public PlayerControl.ePlayerWeaponType GetWeaponType() {
		return weaponType;
	}
}