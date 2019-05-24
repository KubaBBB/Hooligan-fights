﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	[SerializeField]
	private Image healthStats;

	[SerializeField]
	private Image staminaStats;

	public float stamina = 100f;

	public void DisplayHealthStats(float healthValue) {
		healthValue /= 100f;
		healthStats.fillAmount = healthValue;
	}

	public void DisplayStaminaStats() {
		float staminaValue = stamina / 100f;
		staminaStats.fillAmount = staminaValue;
	}
}
