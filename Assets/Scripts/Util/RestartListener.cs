using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartListener : MonoBehaviour
{
    [SerializeField] private KeyCode restartKeyCode = KeyCode.R;
    [SerializeField] private float secondsBeforeCancel = 3f;

    [SerializeField] private TextMeshProUGUI restartConfirmationText = default;

    private bool confirmationShown = false;

    private void Start() {
        ToggleRestartTextState(false);
    }

    private void Update() {
        if(Input.GetKeyDown(restartKeyCode)) {
            if(confirmationShown) {
                RestartGame();
            } else {
                StartCoroutine(ShowConfirmation());
            }
        }   
    }

    private IEnumerator ShowConfirmation() {
        confirmationShown = true;
        ToggleRestartTextState(true);

        yield return new WaitForSeconds(secondsBeforeCancel);

        confirmationShown = false;
        ToggleRestartTextState(false);
    }

    private void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ToggleRestartTextState(bool state) {
        restartConfirmationText.enabled = state;
    }
}
