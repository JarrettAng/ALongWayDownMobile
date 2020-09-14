using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPlayer : MonoBehaviour {
	[SerializeField] private ParticleController particleController;

    [Header("Outcomes")]
    [SerializeField] private bool doNothing = false;

    [Header("Respawn")]
    [SerializeField] private bool respawnAfterHit = false;
    [SerializeField] private Transform respawnPoint;

    [Header("Scene Swap")]
    [SerializeField] private bool switchSceneAfterHit = false;
    [SerializeField] private string sceneToSwitch = "MainMenu";

	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.CompareTag("Respawn")) {
            if(doNothing) {
                return;
            }

            particleController.InstantiateParticleOfType(ParticleType.PLAYER_HIT, transform.position);
			SoundManager.Instance.PlaySound("PlayerHit");

            if(respawnAfterHit) {
                transform.position = respawnPoint.position;
            }
            if(switchSceneAfterHit) {
                SceneManager.LoadScene(sceneToSwitch);
            }
		}
	}
}
