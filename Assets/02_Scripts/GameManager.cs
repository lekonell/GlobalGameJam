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

			ItemManager.Register("Player", new Item("Player", GameObject.Find("Player")));

			ItemManager.Register("Player/L_Weapon", new Item("Player/L_Weapon", GameObject.Find("L_Weapon")));

			ItemManager.Register("Player/R_Weapon", new Item("Player/R_Weapon", GameObject.Find("R_Weapon")));

			ItemManager.Register("PlayerBaseAttackProjectile", new Item("PlayerBaseAttackProjectile", GameObject.Find("PlayerBaseAttackProjectile")));

			//GameObject Objects = GameObject.Find("Objects");
			//for (int i = 0; i < Objects.transform.childCount; i++) {
			//	GameObject ObjectsToBeRegistered = Objects.transform.GetChild(i).gameObject;
			//	ItemManager.Register(ObjectsToBeRegistered.name, ObjectsToBeRegistered);
			//	// ObjectsToBeRegistered.SetActive(false);
			//}

			//Objects.SetActive(false);
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

		public static void ShowPauseOverlay() {
			if (isPaused || isPausingOverlay)
				return;

			isPausingOverlay = true;
			ItemManager.Find("PauseOverlay").SetActive(true);
		}

		public static void HidePauseOverlay() {
			if (!isPaused || !isPausingOverlay)
				return;

			isPausingOverlay = false;
			ItemManager.Find("PauseOverlay").SetActive(false);
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
		}

		public static void EnsureInited() {
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
			EnsureInited();

			if (itemMap.TryGetValue(itemName, out Item ret) == true) {
				return ret.GetItemObject();
			}

			return null;
		}

		public static void Clear() {
			EnsureInited();

			itemMap.Clear();
		}
	}
}