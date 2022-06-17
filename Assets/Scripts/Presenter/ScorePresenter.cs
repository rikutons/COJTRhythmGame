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
    [SerializeField]
    private AudioSource audioSource;

    private float defaultVolume;

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
        defaultVolume = audioSource.volume;
    }

    public void AddScore(string judge)
    {
        switch (judge)
        {
            case "perfect":
                userScore.Score += judgeScore.perfectScore;
                userScore.Combo++;
                audioSource.volume = defaultVolume;
                break;
            case "great":
                userScore.Score += judgeScore.greatScore;
                userScore.Combo++;
                audioSource.volume = defaultVolume;
                break;
            case "good":
                userScore.Score += judgeScore.goodScore;
                userScore.Combo++;
                audioSource.volume = defaultVolume;
                break;
            case "miss":
                userScore.Combo = 0;
                audioSource.volume = defaultVolume * 0.3f;
                break;
        }
    }
}
