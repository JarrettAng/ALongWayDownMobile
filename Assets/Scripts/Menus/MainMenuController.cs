using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scemes")]
    [SerializeField] private string gameScene = "MainGame";
    [SerializeField] private string tutorialScene = "Tutorial";

    [Header("Scene objects")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject firstPlayPanel;
    [SerializeField] private Button quitButton;

    private bool firstPlay = false;

    private void Start() {
        if(Application.platform == RuntimePlatform.WebGLPlayer) {
            quitButton.gameObject.SetActive(false);
        }

        firstPlay = !SaveManager.StoredPlayerData.HasPlayedBefore;

        ToggleCredits(false);
    }

    private void OnDisable() {
        SaveManager.StoredPlayerData.HasPlayedBefore = !firstPlay;
        SaveManager.SaveGameData();
    }

    public void PlayGame() {
        if(firstPlay) {
            firstPlayPanel.SetActive(true);
            mainPanel.SetActive(false);
        } else {
            SceneManager.LoadScene(gameScene);
        }
    }

    public void PlayTutorial() {
        firstPlay = false;
        SceneManager.LoadScene(tutorialScene);
    }

    public void ToggleCredits(bool state) {
        if(state) {
            mainPanel.SetActive(false);
            creditsPanel.SetActive(true);
        } else {
            mainPanel.SetActive(true);
            creditsPanel.SetActive(false);
        }
    }

    public void PlayGameAnyways() {
        firstPlay = false;
        SceneManager.LoadScene(gameScene);
    }

    public void QuitGame() {
        Application.Quit();
    }

    #region Credits
 
    public void OpenJarrettProfile() {
        Application.OpenURL("https://jarrett-ang.itch.io/");
    }

    public void OpenJunRongProfile() {
        Application.OpenURL("https://soulbounded.itch.io/");
    }

    public void OpenZapSplat() {
        Application.OpenURL("https://www.zapsplat.com");
    }
    public void OpenEffects() {
        Application.OpenURL("https://github.com/Elringus/SpriteGlow");
    }

    #endregion
}
