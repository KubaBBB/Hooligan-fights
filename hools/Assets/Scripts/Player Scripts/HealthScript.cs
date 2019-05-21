using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour {

	private EnemyAnimator enemyAnim;
	private NavMeshAgent navAgent;
	private EnemyController EnemyController;

	public float health = 100f;
	public bool isPlayer, isCannibal;
	private bool isDead;

	private EnemyAudio enemyAudio;

	private PlayerStats playerStats;

	public GameObject deathMenu;

	void Awake () {
		if(isCannibal)
		{
			enemyAnim = GetComponent<EnemyAnimator>();
			EnemyController = GetComponent<EnemyController>();
			navAgent = GetComponent<NavMeshAgent>();

			// get enemy audio
			enemyAudio = GetComponentInChildren<EnemyAudio>();
		}

		if(isPlayer)
		{
			playerStats = GetComponent<PlayerStats>();
		}
	}
	
	public void ApplyDamage (float damage) {

		if(isDead)
			return;

		health -= damage;

		if(isPlayer)
		{
			playerStats.DisplayHealthStats(health);
		}

		if(isCannibal)
		{
			if(EnemyController.Enemy_State == EnemyState.PATROL) {
				EnemyController.chaseDistance = 1000f;
			}
		}

		if(health <= 0f) {
			PlayerDied();
			isDead = true;
		}
	}

	void PlayerDied()
	{
		if(isCannibal) {
			navAgent.velocity = Vector3.zero;
			navAgent.isStopped = true;
			EnemyController.enabled = false;

			enemyAnim.Dead();

			//Start Couroutine
			StartCoroutine(DeadSound());
			//Enemy Manager spawn more enemies
			EnemyManager.instance.EnemyDied(true);

		}

		if(isPlayer) {
			GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
			for (int i=0; i<enemies.Length; i++) {
				enemies[i].GetComponent<EnemyController>().enabled = false;
				// call enemy manager to stop spawning
			}
			GetComponent<PlayerMovement>().enabled = false;
			GetComponent<PlayerAttack>().enabled = false;
			GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
			EnemyManager.instance.EnemyDied(true);
		}

		if(tag == Tags.PLAYER_TAG) 
		{
			Cursor.lockState = CursorLockMode.None;
			RestartGame();
		} else {
			ScoreScript.scoreValue++;
			Invoke("TurnOffGameObject", 10f);
		}
	}

	void RestartGame() {
		deathMenu.SetActive(true);
		//UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
	}

	void TurnOffGameObject() {
		gameObject.SetActive(false);
	}

	IEnumerator DeadSound() {
		yield return new WaitForSeconds(0.3f);
		enemyAudio.PlayDeadSound();
	}
}
