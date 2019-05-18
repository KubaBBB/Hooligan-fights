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

	void Awake () {
		if(isCannibal)
		{
			enemyAnim = GetComponent<EnemyAnimator>();
			EnemyController = GetComponent<EnemyController>();
			navAgent = GetComponent<NavMeshAgent>();

			// get enemy audio
		}

		if(isPlayer)
		{

		}
	}
	
	public void ApplyDamage (float damage) {

		if(isDead)
			return;

		health -= damage;

		if(isPlayer)
		{
			// show UI
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

			//Enemy Manager spawn more enemies

		}

		if(isPlayer) {
			GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
			for (int i=0; i<enemies.Length; i++) {
				enemies[i].GetComponent<EnemyController>().enabled = false;
				// call enemy manager to stop spawning

				GetComponent<PlayerMovement>().enabled = false;
				GetComponent<PlayerAttack>().enabled = false;
				GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
			}
		}

		if(tag == Tags.PLAYER_TAG) 
		{
				Invoke("RestartGame", 3f);
		} else {
				Invoke("TurnOffGameObject", 10f);
		}
	}

	void RestartGame() {
		UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
	}

	void TurnOffGameObject() {
		gameObject.SetActive(false);
	}
}
