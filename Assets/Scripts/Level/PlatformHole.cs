using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHole : MonoBehaviour {
	[SerializeField, Tooltip("The walls around the hole (Minimum: 2)")] private GameObject[] walls;
	[SerializeField] private ScorePopUp addScorePopUp;

    private GameManager gameManager;

	private bool playerPassed;

    private void Awake() {
        gameManager = GameManager.Instance;
    }

    private void Start() {
		int numberOfWallsToRemove = Random.Range(1, walls.Length + 1);

		List<GameObject> wallsRemaining = walls.ToList();

		for(int current = 0; current < numberOfWallsToRemove; current++) {
			if(wallsRemaining.Count <= 0) {
				Debug.LogWarning("There are less than two walls next to the hole! Add at least 2!");
				break;
			}

			int index = Random.Range(0, wallsRemaining.Count);
			wallsRemaining[index].SetActive(false);
			wallsRemaining.RemoveAt(index);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(playerPassed) {
			return;
		}

		if(other.gameObject.CompareTag("Player")) {
			EventManager.OnPlatformPass?.Invoke();
			EventManager.OnPlayerScore?.Invoke(gameManager.HoleScoreIncrease);

			ScorePopUp popUp = Instantiate(addScorePopUp, transform.position, Quaternion.identity);
			popUp.InitializePopUp(gameManager.HoleScoreIncrease);
			playerPassed = true;
		}
	}
}
