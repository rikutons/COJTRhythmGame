using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultPresenter : MonoBehaviour
{
    [SerializeField]
    private DataModel.UserScore userScore;
    [SerializeField]
    private MasterData.JudgeScore judgeScore;
    [SerializeField]
    private MasterData.RankPercents rankPercents;
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private GameObject APText;
    [SerializeField]
    private GameObject FCText;
    [SerializeField]
    private TextMeshProUGUI result;
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private TextMeshProUGUI rank;
    [SerializeField]
    private TextMeshProUGUI perfect;
    [SerializeField]
    private TextMeshProUGUI great;
    [SerializeField]
    private TextMeshProUGUI good;
    [SerializeField]
    private TextMeshProUGUI miss;

    public void Show()
    {
        resultPanel.SetActive(true);
        perfect.text = userScore.PerfectCount.ToString();
        great.text = userScore.GreatCount.ToString();
        good.text = userScore.GoodCount.ToString();
        miss.text = userScore.MissCount.ToString();
        int notesCount = userScore.PerfectCount + userScore.GreatCount + userScore.GoodCount + userScore.MissCount;
        int perfectScore = judgeScore.perfectScore * notesCount;
        float achivementRate = 0;
        if(perfectScore != 0)
            achivementRate = (float)userScore.Score / perfectScore * 100;
        score.text = String.Format("{0:0.0000}",achivementRate) + "%";
        if(achivementRate >= rankPercents.clearPercent)
        {
            result.text = "CLEAR!!";
        }
        else
        {
            result.text = "FAILED...";
        }
        MasterData.Rank now_rank = null;
        foreach(var rankP in rankPercents.ranks)
        {
            now_rank = rankP;
            if(rankP.percent <= achivementRate)
                break;
        }
        rank.text = now_rank.rankName;
        rank.color = now_rank.color;

        if(notesCount != userScore.PerfectCount)
        {
            APText.SetActive(false);
        }
        if(notesCount != userScore.Combo)
        {
            FCText.SetActive(false);
        }
        foreach (var layoutGroup in resultPanel.GetComponentsInChildren<LayoutGroup>())
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());
        }
    }
}
