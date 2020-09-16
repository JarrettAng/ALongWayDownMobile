using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pauseMenu = default;
    [SerializeField] private GameObject buttonsPanel = default;
    [SerializeField] private GameObject settingsPanel = default;

    private void Awake() {
        EventManager.OnGameOver += DisablePause;
    }

    public void TogglePauseMenu(bool state) {
        pauseMenu.SetActive(state);

        Time.timeScale = (state == true) ?  0 : 1;
    }

    public void OpenSettings() {
        ToggleButtonsPanel(false);
        ToggleSettingsPanel(true);
    }

    public void CloseSettings() {
        ToggleButtonsPanel(true);
        ToggleSettingsPanel(false);
    }

    private void ToggleButtonsPanel(bool state) {
        buttonsPanel.SetActive(state);
    }

    private void ToggleSettingsPanel(bool state) {
        settingsPanel.SetActive(state);
    }

    private void DisablePause() {
        gameObject.SetActive(false);
    }
}
