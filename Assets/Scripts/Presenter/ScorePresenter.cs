using System;
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
        userScore.onScoreChanged.Subscribe(score => scoreText.text = "Score: " + score.ToString());
        userScore.onComboChanged.Subscribe(combo => comboText.text = "Combo: " + combo.ToString());

        userScore.onPerfectCountChanged.Subscribe(_ => userScore.Score += judgeScore.perfectScore);
        userScore.onGreatCountChanged.Subscribe(_ => userScore.Score += judgeScore.greatScore);
        userScore.onGoodCountChanged.Subscribe(_ => userScore.Score += judgeScore.goodScore);

        userScore.onSuccess.Subscribe(_ => userScore.Combo++);
        userScore.onMissCountChanged.Subscribe(_ => userScore.Combo = 0);
    }
}