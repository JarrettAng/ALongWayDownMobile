using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour {
    [SerializeField] private GameObject restartListener;

	[SerializeField] private GameObject gameOverCanvas;
	[SerializeField] private GameObject gameOverHighscoreEffect;

	[SerializeField] private GameObject newHighscoreTxt;

	[SerializeField] private TextMeshProUGUI scoreTxt;
	[SerializeField] private TextMeshProUGUI highscoreTxt;
	[SerializeField] private Animator scoreTxtAnim;

	private SoundManager soundManager;

	private void Awake() {
		EventManager.OnGameOver += GameOver;

		soundManager = SoundManager.Instance;
	}

    private void GameOver() {
        restartListener.SetActive(false);

        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;

        HandleSound();

        HandleScore();

        int highscore;
        int currentScore;

        void HandleScore() {
            int oldHighscore = GameManager.Highscore;
            highscoreTxt.text = "HIGHSCORE:  " + oldHighscore;

            GameManager.UpdateHighscore();
            SaveManager.SaveGameData();

            currentScore = GameManager.CurrentScore;
            highscore = GameManager.Highscore;

            StartCoroutine(DisplayScore());
        }

        void HandleSound() {
            soundManager.PlaySound("GameOver");
            soundManager.StopSound("GameMusic");
        }

        IEnumerator DisplayScore() {

            yield return new WaitForSecondsRealtime(0.2f);
            StartCoroutine(SpaceToRetry());

            int currentNumberDisplayed = 0;
            scoreTxt.text = currentNumberDisplayed.ToString();

            while(currentNumberDisplayed < currentScore) {
                currentNumberDisplayed++;

                // Fast count if player scored too much points.
                if(currentNumberDisplayed > 100) {
                    currentNumberDisplayed = currentScore;
                }

                scoreTxt.text = currentNumberDisplayed.ToString();

                if(currentNumberDisplayed == currentScore) {
                    scoreTxtAnim.SetTrigger("BigPop");
                }

                yield return new WaitForSecondsRealtime(0.01f);
            }

            if(currentScore == highscore) {
                newHighscoreTxt.SetActive(true);
                gameOverHighscoreEffect.SetActive(true);
                highscoreTxt.text = "HIGHSCORE:  " + highscore;

                soundManager.PlaySound("NewHighscore");
            }
        }
    }

    private IEnumerator SpaceToRetry() {
        yield return new WaitForSecondsRealtime(0.5f);
        while(true) {
            if(Input.GetKey(KeyCode.Space)) {
                Retry();
            }

            yield return new WaitForEndOfFrame();
        }
    }

	public void Retry() {
		Time.timeScale = 1;
		SceneManager.LoadScene("MainGame");
	}

	public void Menu() {
		Time.timeScale = 1;
		SceneManager.LoadScene("MainMenu");
	}
}
