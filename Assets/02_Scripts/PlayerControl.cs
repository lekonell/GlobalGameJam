using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using DG.Tweening;
using static PlayerControl;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {
	public Animator animator;

	public enum ePlayerWeaponType {
		WeaponTypeMelee,
		WeaponTypeRange
	};

	public enum ePlayerAttackType {
		AttackTypeMelee,
		AttackTypeRange
	};

	public enum ePlayerMoveType {
		MoveTypeUp,
		MoveTypeLeft,
		MoveTypeRight,
		MoveTypeDown
	};

	public enum ePlayerDirection {
		DirectionLeft,
		DirectionRight
	};

	public PlayerWeaponManager playerWeaponManager;
	public Animator playerMeleeAnimator;

	private float playerHP = 5.0f;
	private float playerMaxHP = 5.0f;

	private bool isPlayerSuperArmor = true;
	private bool isPlayerStunned = false;
	private bool isPlayerWeaponChangeCooldown = false;
	private bool isPlayerBaseAttackCooldown = false;
	private ePlayerDirection playerDirection = ePlayerDirection.DirectionLeft;

	private float moveSpeed = 5.0f;
	private float moveMultiplier = 5.0f;

	private int playerGold = 0;

	public PlayerControl SetPlayerGold(int _playerGold) {
		int _playerGoldOld = playerGold;
		playerGold = _playerGold;

		GameObject.Find("UI_InGameScene").GetComponent<UI_InGameScene>().UpdateGold(_playerGoldOld, _playerGold);

		return this;
	}

	public int GetPlayerGold() {
		return playerGold;
	}

	public PlayerControl SetPlayerHP(float _playerHP) {
		float _playerOldHP = playerHP;

		playerHP = _playerHP;

		if (playerHP > playerMaxHP)
			playerHP = playerMaxHP;

		if (_playerOldHP > playerHP) {
			SetPlayerSuperArmorState(true);
			Camera.main.GetComponent<CameraManager>().SetCameraFixed(true);

			float cameraX = Camera.main.transform.position.x;

			Camera.main.transform.DOLocalMoveX(cameraX - 0.2f, 0.12f).OnComplete(() => {
				;
				Camera.main.transform.DOLocalMoveX(cameraX + 0.4f, 0.12f).OnComplete(() => {
					Camera.main.transform.DOLocalMoveX(cameraX - 0.2f, 0.12f).OnComplete(() => {
						Camera.main.GetComponent<CameraManager>().SetCameraFixed(false);
					});
				});
			});
		}

		if (playerHP <= 0) {
			playerHP = 0;

			GameObject.Find("UI_InGameScene").GetComponent<UI_InGameScene>().UpdateUI_PlayerDie();
		}


		Managers.UI.UpdateUI();

		return this;
	}

	public PlayerControl SetPlayerSuperArmorState(bool _playerSuperArmorState) {
		isPlayerSuperArmor = _playerSuperArmorState;

		if (isPlayerSuperArmor) {
			isPlayerStunned = true;
			StartCoroutine(PlayerSuperArmorProcess());
		}

		return this;
	}

	public bool GetPlayerSuperArmorState() {
		return isPlayerSuperArmor;
	}

	private IEnumerator PlayerSuperArmorProcess() {
		yield return new WaitForSeconds(1.0f);
		isPlayerStunned = false;
		yield return new WaitForSeconds(1.0f);
		isPlayerSuperArmor = false;
	}

	public PlayerControl SetPlayerMaxHP(float _playerMaxHP) {
		playerMaxHP = _playerMaxHP;
		return this;
	}

	public PlayerControl SetPlayerMoveSpeed(float _playerMoveSpeed) {
		moveSpeed = _playerMoveSpeed;
		return this;
	}

	public float GetPlayerHP() {
		return playerHP;
	}

	public float GetPlayerMaxHP() {
		return playerMaxHP;
	}

	public float GetPlayerMoveSpeed() {
		return moveSpeed;
	}

	private void Start() {
		animator = GetComponent<Animator>();
		playerWeaponManager = GetComponent<PlayerWeaponManager>();
		playerWeaponManager.SetWeaponType(ePlayerWeaponType.WeaponTypeMelee);

		playerMeleeAnimator = transform.Find("MeleeAnimator").GetComponent<Animator>();
	}

	void Update() {
		if (isPlayerStunned) {
			animator.SetFloat("RunState", 0);
			return;
		}

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "InGameA0" && scene.name != "InGameA1")
        {
            return;
        }

        Vector2 moveVector = Vector2.zero;

		/*
		 * Root World:
		 *		WASD로 이동한다.
		 */
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			moveVector += Vector2.up;
		}

		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			/*
			 * UpdateDirection() 함수를 호출해 transform.Rotate로 Player의 방향을 전환시킨다.
			 * 이 때 Player의 이동방향은 transform의 결과를 따르는 것으로 보인다.
			 * * 즉, transform 이전의 우측 이동은 transform 이후의 좌측 이동과 동일하다.
			 * 따라서 왼쪽, 오른쪽 이동 모두 같은 방향(playerDirectionLeft)으로 이동시키도록 구현한다.
			 */

			moveVector += Vector2.left;
			UpdateDirection(ePlayerDirection.DirectionLeft);
		}

		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			moveVector += Vector2.left;
			UpdateDirection(ePlayerDirection.DirectionRight);
		}

		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			moveVector += Vector2.down;
		}

		if (moveVector.magnitude > 0) {

			moveVector = moveVector.normalized;
			animator.SetFloat("RunState", 0.5f);

			transform.Translate(moveVector * moveSpeed * moveMultiplier * Time.deltaTime);
		}
		else {
			animator.SetFloat("RunState", 0);
		}

		/*
		 * GetMouseButton(0): 마우스 왼쪽 클릭
		 * GetMouseButton(1): 마우스 오른쪽 클릭
		 * GetMouseButton(2): 마우스 휠 클릭
		 * GetMouseButton(3-6): 마우스 추가 버튼 클릭
		 * 
		 * Root World:
		 *		마우스 좌클릭으로 공격한다.
		 *		마우스 우클릭으로 무기를 교체한다.
		 */
		if (Input.GetMouseButton(0)) {
			PlayerBaseAttack(playerWeaponManager.GetWeaponType());
			return;
		}
		else if (Input.GetMouseButton(1)) {
			if (isPlayerWeaponChangeCooldown)
				return;

			isPlayerWeaponChangeCooldown = true;
			playerWeaponManager.SetWeaponType(1 - playerWeaponManager.GetWeaponType());
			StartCoroutine(PlayerWeaponChangeCooldownProcess(1.0f));
		}
	}

	void UpdateDirection(ePlayerDirection updateDirection) {
		if (playerDirection == updateDirection)
			return;

		transform.Rotate(0, 180.0f, 0);
		playerDirection = updateDirection;
	}

	private void PrintVector2(string vectorName, Vector2 vector) {
		Debug.Log("[PrintVector2] " + vectorName + "(" + vector.x + ", " + vector.y + ")");
	}

	private void PrintVector3(string vectorName, Vector3 vector) {
		Debug.Log("[PrintVector3] " + vectorName + "(" + vector.x + ", " + vector.y + ", " + vector.z + ")");
	}

	private void PlayerBaseAttack(ePlayerWeaponType playerAttackType) {
		if (ItemManager.Find("Player/L_Weapon") == null || GameManager.GameManager.isPaused || GameManager.GameManager.isPausingOverlay)
			return;

		if (isPlayerBaseAttackCooldown)
			return;

		switch (playerAttackType) {
			case ePlayerWeaponType.WeaponTypeMelee:
				animator.SetFloat("AttackState", 0);
				animator.SetFloat("NormalState", 0);
				animator.SetTrigger("Attack");

				transform.DOMove(transform.position, 0.075f).OnComplete(() => {
					playerMeleeAnimator.SetTrigger("MeleeAttack");
				});

				ItemManager.Find("Player/R_Weapon").GetComponent<PlayerBaseAttack>().SetDamage(playerWeaponManager.GetWeaponDamage()).SetValid(true);

				break;
			case ePlayerWeaponType.WeaponTypeRange:
				animator.SetFloat("AttackState", 0);
				animator.SetFloat("NormalState", 0.5f);
				animator.SetTrigger("Attack");

				Vector2 playerPosition = transform.position;
				Vector2 inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 dist = (inputPosition - playerPosition).normalized;

				transform.DOMove(transform.position, 0.4f).OnComplete(() => {
					GameObject playerProjectile = Instantiate(ItemManager.Find("PlayerBaseAttackProjectile"), ItemManager.Find("Player/L_Weapon").transform.position, Quaternion.identity);

					PlayerAttackManager playerAttackManager = playerProjectile.GetComponent<PlayerAttackManager>();
					playerAttackManager.SetLifespan(1.2f);
					playerAttackManager.SetDirection(dist);
					playerAttackManager.SetSpeed(10.0f);
					playerAttackManager.SetValid(true);
					playerAttackManager.SetDamage(playerWeaponManager.GetWeaponDamage());
				});
				break;
		}

		isPlayerBaseAttackCooldown = true;
		StartCoroutine(PlayerBaseAttackCooldownProcess(playerWeaponManager.GetWeaponCooldown()));
	}

	private IEnumerator PlayerBaseAttackCooldownProcess(float cooldown) {
		yield return new WaitForSeconds(cooldown);
		isPlayerBaseAttackCooldown = false;
	}

	private IEnumerator PlayerWeaponChangeCooldownProcess(float cooldown) {
		yield return new WaitForSeconds(cooldown);
		isPlayerWeaponChangeCooldown = false;
	}
}