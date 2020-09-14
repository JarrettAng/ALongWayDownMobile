using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : Singleton<HealthUI> {
	[SerializeField, Tooltip("Sprites for health.")] private Sprite fullHeart, emptyHeart;
	[SerializeField, Tooltip("Image elements for the player's health.")] private Image[] hearts;

	public void UpdateUIHeart(int currentHealth) {
		// Go through all the heart images
		for(int i = 0; i < hearts.Length; i++) {
			// Update the hearts based on the current health of the player.
			if(i < currentHealth) {
				hearts[i].sprite = fullHeart;
			} else {
				hearts[i].sprite = emptyHeart;
			}
		}
	}
}
