using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoKillCombo : MonoBehaviour
{
    [SerializeField] private int bonusIncrease = 3;
    [SerializeField] private int maxBonus = 50;
    [SerializeField] private int originalHoleScore;

    private GameManager gameManager;

    private void Awake() {
        gameManager = GameManager.Instance;

        EventManager.OnEnemyKill += ResetHoleScore;
        EventManager.OnPlatformPass += IncreaseHoleScore;
    }

    private void Start() {
        // Hole will increase score to default
        gameManager.HoleScoreIncrease -= bonusIncrease;

        originalHoleScore = gameManager.HoleScoreIncrease;
    }

    private void IncreaseHoleScore() {
        if(gameManager.HoleScoreIncrease >= maxBonus) {
            return;
        }

        gameManager.HoleScoreIncrease += bonusIncrease;
    }

    private void ResetHoleScore() {
        gameManager.HoleScoreIncrease = originalHoleScore;
    }
}
