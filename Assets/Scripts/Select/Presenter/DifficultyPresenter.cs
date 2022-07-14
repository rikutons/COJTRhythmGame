using UnityEngine;
using TMPro;
using UniRx;

public class DifficultyPresenter : MonoBehaviour
{
    [SerializeField]
    SelectMenuData selectMenuData;
    [SerializeField]
    TextMeshProUGUI rankText;
    int index = 0;
    readonly string[] difficulties = { "A", "B", "C", "F" };
    void Start()
    {
        selectMenuData.onSelectedRankChanged.Subscribe(rank => rankText.text = rank);
        ApplyIndex();
    }

    void ApplyIndex()
    {
        index += 4;
        index %= 4;
        selectMenuData.SelectedRank = difficulties[index];
    }

    public void OnPressLeft()
    {
        index--;
        ApplyIndex();
    }

    public void OnPressRight()
    {
        index++;
        ApplyIndex();
    }
}
