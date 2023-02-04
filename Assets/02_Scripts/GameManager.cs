using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameManager {
	/*
	 * GameManager는 CamaraManager로부터 Init()된다.
	 * Scripting Execution Order에 따라 우선순위는 EventSystem > CameraManager > [Other Scripts]
	 */
	public static class GameManager {
		public static bool isInited = false;
		public static bool isPaused = false;
		public static bool isPausingOverlay = false;

		public static void Init() {
			if (isInited)
				return;

			isInited = true;

			/*
			if (!ItemManager.Register("Player", new Item("Player", GameObject.Find("UnitRoot")))) {
				Debug.Log("GameManager->failed at Register(Player)");
			}

			if (!ItemManager.Register("Player/L_Weapon", new Item("Player/L_Weapon", GameObject.Find("L_Weapon")))) {
				Debug.Log("GameManager->failed at Register(L_Weapon)");
			}

			if (!ItemManager.Register("Player/R_Weapon", new Item("Player/R_Weapon", GameObject.Find("R_Weapon")))) {
				Debug.Log("GameManager->failed at Register(R_Weapon)");
			}

			if (!ItemManager.Register("PlayerBaseAttackProjectile", new Item("PlayerBaseAttackProjectile", GameObject.Find("PlayerBaseAttackProjectile")))) {
				Debug.Log("GameManager->failed at Register(PlayerBaseAttackProjectile)");
			}
			*/

			Debug.Log("GameManager Inited");
		}

		public static void Clear() {
			isInited = false;
			ItemManager.Clear();
		}

		public static void Pause() {
			if (isPaused || isPausingOverlay)
				return;

			isPaused = true;
		}

		public static void Resume() {
			if (!isPaused || !isPausingOverlay)
				return;

			isPaused = false;
		}

		public static bool GetPausedState() {
			return isPaused;
		}

		public static bool GetPausingOverlayState() {
			return isPausingOverlay;
		}
	}

	public class Item {
		private string itemName { get; }
		private GameObject itemObject { get; }

		public Item(string _itemName, GameObject _itemObject) {
			itemName = _itemName;
			itemObject = _itemObject;
		}

		public string GetItemName() {
			return itemName;
		}

		public GameObject GetItemObject() {
			return itemObject;
		}
	}

	public static class ItemManager {
		public static Dictionary<string, Item> itemMap;
		public static bool isInited = false;

		public static void Init() {
			if (isInited)
				return;

			isInited = true;
			itemMap = new Dictionary<string, Item>();

			Debug.Log("ItemManager Inited");
		}

		public static void EnsureInited() {
			GameManager.Init();

			if (!isInited) 
				Init();
		}

		public static bool Register(string itemName, Item itemObject, bool isOverwritable = false) {
			EnsureInited();

			if (!isOverwritable && itemMap.ContainsKey(itemName)) {
				return false;
			}

			if (itemMap.ContainsKey(itemName)) {
				Remove(itemName);
			}

			itemMap.Add(itemName, itemObject);
			return true;
		}

		public static bool Remove(string itemName) {
			EnsureInited();

			if (itemMap.ContainsKey(itemName)) {
				itemMap.Remove(itemName);
				return true;
			}

			return false;
		}

		public static GameObject Find(string itemName) {
			if (itemName == "Player") {
				return Camera.main.GetComponent<CameraManager>().player;
			}
			else if (itemName == "Player/L_Weapon") {
				return GameObject.Find("L_Weapon");
			}
			else if (itemName == "Player/R_Weapon") {
				return GameObject.Find("R_Weapon");
			}
			else if (itemName == "PlayerBaseAttackProjectile") {
				return GameObject.Find("PlayerBaseAttackProjectile");
			}
			else if (itemName == "EnemyRangeAttackProjectile") {
				return GameObject.Find("EnemyRangeAttackProjectile");
			}

			return null;
		}

		public static void Clear() {
			EnsureInited();

			isInited = false;
			itemMap.Clear();
		}
	}
}