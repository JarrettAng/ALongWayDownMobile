using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager> {
	[SerializeField] private TextMeshProUGUI scoreTxt;
	[SerializeField] private Animator scoreAnim;

	[SerializeField, Tooltip("What music should play")] private string musicToPlay = "GameMusic";
	[SerializeField, Tooltip("What music should play")] private bool stopPlayingLastMusic = true;
	[SerializeField, Tooltip("What music should play")] private string musicToStop = "MenuMusic";

    [Header("Scores")]
    public int EnemyScoreIncrease = 35;
    public int HoleScoreIncrease = 10;

    public GameObject Player;

	private static int currentScore;
	private static int highscore;

	private SoundManager soundManager;

	private bool gameOver = false;

	#region Properties

	public static int CurrentScore { get => currentScore; set => currentScore = value; }

	public static int Highscore { get => highscore; set => highscore = value; }

	#endregion

	private void Awake() {
		EventManager.OnPlayerScore += IncreaseScore;

		soundManager = SoundManager.Instance;
	}

	private void Start() {
		CurrentScore = 0;
        Highscore = SaveManager.StoredPlayerData.Highscore;
		UpdateScoreTxt();

		soundManager.PlaySound(musicToPlay);

        if(stopPlayingLastMusic) {
            soundManager.StopSound(musicToStop);
        }
    }

	private void IncreaseScore(int amount) {
		CurrentScore += amount;
		UpdateScoreTxt();

		soundManager.PlaySound("ScorePoint");

		scoreAnim.SetTrigger("AddScore");
	}

	private void UpdateScoreTxt() {
		scoreTxt.text = CurrentScore.ToString();
	}

	public static void UpdateHighscore() {
        if(currentScore > highscore) {
            highscore = currentScore;
            SaveManager.StoredPlayerData.Highscore = highscore;
        }
    }
}
