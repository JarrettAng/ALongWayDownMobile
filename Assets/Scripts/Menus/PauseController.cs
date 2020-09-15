using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pauseMenu = default;

    private void Awake() {
        EventManager.OnGameOver += DisablePause;
    }

    public void TogglePauseMenu(bool state) {
        pauseMenu.SetActive(state);

        Time.timeScale = (state == true) ?  0 : 1;
    }

    private void DisablePause() {
        gameObject.SetActive(false);
    }
}
