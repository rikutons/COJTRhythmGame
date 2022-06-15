using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField]
    private DataModel.UserScore userScore;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI comboText;
    [SerializeField]
    private MasterData.JudgeScore judgeScore;
    void Start()
    {
        userScore.onScoreChanged.Subscribe(score =>
        {
            scoreText.text = "Score: " + score.ToString();
        });
        userScore.onComboChanged.Subscribe(combo =>
        {
            comboText.text = "Combo: " + combo.ToString();
        });
    }

    public void AddScore(string judge)
    {
        switch (judge)
        {
            case "perfect":
                userScore.Score += judgeScore.perfectScore;
                userScore.Combo++;
                break;
            case "great":
                userScore.Score += judgeScore.greatScore;
                userScore.Combo++;
                break;
            case "good":
                userScore.Score += judgeScore.goodScore;
                userScore.Combo++;
                break;
            case "miss":
                userScore.Combo = 0;
                break;
        }
    }
}
