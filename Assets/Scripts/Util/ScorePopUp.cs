using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePopUp : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI scoreTxt;

    [SerializeField] private float deleteAfterSeconds = 2f;

	public void InitializePopUp(int scoreAmount = 10) {
		scoreTxt.text = "+" + scoreAmount;

        Invoke("DeleteSelf", deleteAfterSeconds);
	}

    private void DeleteSelf() {
        Destroy(gameObject);
    }
}
